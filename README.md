# CrossPlatformLabs
Варіант 47

## Запуск
Для того, щоб запустити лабораторну, варто виконати наступні команди.

Для збірки проекту:
```bash
dotnet build Build.proj -t:Build -p:Solution=Lab1
```
Для запуску проекту:
```bash
dotnet build Build.proj -t:Run -p:Solution=Lab1
```
Для запуску тестів:
```bash
dotnet build Build.proj -t:Test -p:Solution=Lab1
```
*Lab1 може бути замінена на Lab2, Lab3 і т.д.

