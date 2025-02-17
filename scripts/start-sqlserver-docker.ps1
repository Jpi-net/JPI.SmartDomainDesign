param (
    [string]$ContainerName = "sqlserver-sdd",
    [string]$ImageName = "mcr.microsoft.com/mssql/server:2022-latest",
    [string]$VolumeName = "sqlserverdata-sdd",
    [string]$Password = "YourStrong@Password",
    [string]$Port = "1433"
)

function Ensure-DockerDesktopRunning {
    param (
        [int]$SleepDuration = 10
    )

    Write-Host "Checking if Docker Desktop is running..."
    $dockerProcess = Get-Process -Name "Docker Desktop" -ErrorAction SilentlyContinue

    if (-not $dockerProcess) {
        Write-Host "Docker Desktop is not running. Starting Docker Desktop..."
        Start-Process -FilePath "C:\Program Files\Docker\Docker\Docker Desktop.exe" -NoNewWindow
        Start-Sleep -Seconds $SleepDuration
    }
}

function Wait-ForDockerInitialization {
    param (
        [int]$MaxAttempts = 20,
        [int]$SleepDuration = 5
    )

    while ($MaxAttempts -gt 0) {
        $dockerInfo = docker info --format '{{.ServerErrors}}' 2>&1

        if ($dockerInfo -eq "[]") {
            Write-Host "Docker Desktop is fully initialized."
            return
        } else {
            Write-Host "Waiting for Docker Desktop to be fully initialized... ($MaxAttempts attempts remaining)"
            Start-Sleep -Seconds $SleepDuration
            $MaxAttempts--
        }
    }

    if ($MaxAttempts -eq 0) {
        Write-Host "ERROR: Docker Desktop failed to initialize after maximum attempts." -ForegroundColor Red
        throw "Docker Desktop failed to initialize after maximum attempts."
        exit 1
    }

    Write-Host "Docker Desktop is ready."
}

function Ensure-DockerVolume {
    param (
        [string]$VolumeName
    )

    $volume = docker volume ls --filter "name=$VolumeName" --format "{{.Name}}"

    if ($volume -eq $null -or $volume -ne $VolumeName) {
        Write-Host "Docker volume '$VolumeName' not found. Creating it now..."
        docker volume create $VolumeName | Out-Null

        if ($LASTEXITCODE -ne 0) {
            throw "Failed to create Docker volume '$VolumeName'."
        }

        Write-Host "Docker volume '$VolumeName' has been successfully created."
    } else {
        Write-Host "Docker volume '$VolumeName' already exists."
    }
}

function Start-SqlServerContainer {
    param (
        [string]$ContainerName,
        [string]$ImageName,
        [string]$Password,
        [string]$Port
    )

    $container = docker ps -a --filter "name=$ContainerName" --format "{{.Names}}"

    if ($container -eq $null) {
        Write-Host "Creating and starting a new SQL Server container..."
        Start-Process -NoNewWindow -FilePath "docker" -ArgumentList @(
            "run",
            "-e", "ACCEPT_EULA=Y",
            "-e", "SA_PASSWORD=$Password",
            "-p", "$Port:$Port",
            "--name", $ContainerName,
            "-d",
            $ImageName,
            "-v", "${VolumeName}:/var/opt/mssql"
        )
    } else {
        Write-Host "Starting the existing SQL Server container..."
        Start-Process -NoNewWindow -FilePath "docker" -ArgumentList "start", $ContainerName
    }
}

# Main script execution
try {
    Write-Host "Ensuring Docker Desktop is running..."
    Ensure-DockerDesktopRunning -SleepDuration 10

    Write-Host "Waiting for Docker Desktop to initialize..."
    Wait-ForDockerInitialization -MaxAttempts 10 -SleepDuration 5

    Write-Host "Waiting for Docker Volume to initialize..."
    Ensure-DockerVolume -VolumeName $VolumeName

    Write-Host "Starting SQL Server container..."
    Start-SqlServerContainer -ContainerName $ContainerName -ImageName $ImageName -Password $Password -Port $Port
    exit 0
} catch {
    Write-Host "ERROR: $($_.Exception.Message)" -ForegroundColor Red
    exit 1
}