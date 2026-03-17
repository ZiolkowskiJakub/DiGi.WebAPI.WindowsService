# DiGi.WebAPI.WindowsService

## Operation

cd "C:\ProgramData\DiGi\DiGi.WebAPI.WindowsService"

.\DiGi.WebAPI.WindowsService.exe --install
Start-Service -Name DiGi.WebAPI.WindowsService

Restart-Service -Name DiGi.WebAPI.WindowsService 

Stop-Service -Name DiGi.WebAPI.WindowsService
.\DiGi.WebAPI.WindowsService.exe --uninstall

## Maintenance

Get-Service -Name DiGi.WebAPI.WindowsService
sc delete DiGi.WebAPI.WindowsService
Remove-Service -Name DiGi.WebAPI.WindowsService 

## Endpoint check:
http://localhost:5010/swagger/index.html