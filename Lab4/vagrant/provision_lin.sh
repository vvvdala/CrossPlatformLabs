
sudo apt-get update

sudo apt-get install -y wget apt-transport-https software-properties-common

wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
sudo apt-get update

sudo apt-get install -y dotnet-sdk-8.0

mkdir -p ~/.nuget/NuGet
cat <<EOF > ~/.nuget/NuGet/NuGet.Config
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <packageSources>
    <add key="BaGet" value="http://192.168.56.1:5555/v3/index.json" />
  </packageSources>
</configuration>
EOF

curl http://192.168.56.1:5555/v3/index.json -k

dotnet tool install --global App --add-source http://192.168.56.1:5555/v3/index.json --version 1.0.0
