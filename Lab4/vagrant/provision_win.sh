
if (-Not (Get-Command choco -ErrorAction SilentlyContinue)) {
    Set-ExecutionPolicy Bypass -Scope Process -Force
    [System.Net.ServicePointManager]::SecurityProtocol = [System.Net.SecurityProtocolType]::Tls12
    iex ((New-Object System.Net.WebClient).DownloadString('https://chocolatey.org/install.ps1'))
}

choco install -y dotnet-sdk --version 8.0.100

$NugetConfigPath = [System.IO.Path]::Combine($env:USERPROFILE, ".nuget", "NuGet")
if (!(Test-Path -Path $NugetConfigPath)) {
    New-Item -ItemType Directory -Force -Path $NugetConfigPath
}

$NugetConfig = @"
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <packageSources>
    <add key="BaGet" value="http://192.168.56.1:5555/v3/index.json" />
  </packageSources>
</configuration>
"@

Set-Content -Path "$NugetConfigPath\\NuGet.Config" -Value $NugetConfig -Encoding UTF8

Invoke-WebRequest -Uri "http://192.168.56.1:5555/v3/index.json" -UseBasicParsing -SkipCertificateCheck

dotnet tool install --global App --add-source http://192.168.56.1:5555/v3/index.json --version 1.0.0
