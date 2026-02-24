cd "D:\Nextcloud\Work\DigiProject\Software\DiGi.WebAPI.WindowsService"
.\DiGi.WebAPI.WindowsService.exe --install
Start-Service -Name DiGi.WebAPI.WindowsService

Restart-Service -Name DiGi.WebAPI.WindowsService 

Stop-Service -Name DiGi.WebAPI.WindowsService
.\DiGi.WebAPI.WindowsService.exe --uninstall