# DiGi.WebAPI.WindowsService

Windows Service host for the DiGi WebAPI. All commands assume an **elevated PowerShell** prompt (Run as Administrator) with the service binaries deployed to the install directory.

> **Configuration**
> - **Service name:** `DiGi.WebAPI.WindowsService`
> - **Install directory:** `%ProgramData%\DiGi\DiGi.WebAPI.WindowsService`
> - **Listening port:** `5010` (must match `appsettings.json`)

## Installation

Open the inbound firewall port (run once; keep in sync with the listening port):

```powershell
New-NetFirewallRule -DisplayName "DiGi WebAPI" -Direction Inbound -LocalPort 5010 -Protocol TCP -Action Allow
```

## Operation

```powershell
cd "$env:ProgramData\DiGi\DiGi.WebAPI.WindowsService"

# Register and start
.\DiGi.WebAPI.WindowsService.exe --install
Start-Service -Name DiGi.WebAPI.WindowsService

# Restart after a redeploy
Restart-Service -Name DiGi.WebAPI.WindowsService

# Stop and unregister
Stop-Service -Name DiGi.WebAPI.WindowsService
.\DiGi.WebAPI.WindowsService.exe --uninstall
```

## Maintenance

```powershell
Get-Service -Name DiGi.WebAPI.WindowsService

# Force-remove registration (only if --uninstall fails)
Remove-Service -Name DiGi.WebAPI.WindowsService   # PowerShell 6+
sc.exe delete DiGi.WebAPI.WindowsService          # legacy fallback
```

## Endpoint check

- Local: <http://localhost:5010/swagger/index.html>
- Public: <https://api.digiproject.uk/information/controllers>
