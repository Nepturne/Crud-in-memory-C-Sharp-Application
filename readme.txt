    Para começar precisaremos de algumas ferramentas:

    1) Visual Studio Code: code.visualstudio.com
    -> Donwload for Windows.

    2) Docker: docker.com/get-started
    -> Get Started
    -> Rolar para baixo: Donwload Desktop and Take a Tutorial
    -> Donwload Docker Desktop for Windows

    3) Kitematic: kitematic.com
    -> Ir em github.com/docker/kitematic/releases
    -> Fazer o donwload do Kitematic-0.17.9-Windows.zip
    -> Para que usemos o docker de forma mais visual.

    4) Azure Data Studio: docs.microsoft.com/pt-br/azure-data-studio/donwload
    -> Instalador do usuário (recomendado)

    5) .NET Framework: dotnet.microsoft.com/apps/aspnet
    -> Descer até donwload
    -> Clicar em Donwload .NET Core SDK

    -> Instalamos:
    1) Visual Studio Code. Marcar todas as opções
    2) Azure Data Studio. Não executa no final
    3) Dotnet Sdk 3.0.100 ou 3.0.103
    4) Microsoft Store - Windows Terminal -> Install
    5) Instalar as extensions no VSCode, C# e Azure Tools
    6) Instalar Docker
        - Ele vai reiniciar a máquina 2 vezes
        - Criar o docker id
        - Jogar o Kitematic na pasta do ../ Docker

    Caso já tenha uma versão do SDK do .NET Core
    na máquina e quisermos alterar
    para o projeto podemos fazer:
    https://stackoverflow.com/questions/42077229/switch-between-dotnet-core-sdk-versions

    Você pode fazer isso com um global.jsonarquivo na raiz do seu projeto:

    Verifique a lista de SDKs em sua máquina:
    dotnet --list-sdks
    Você verá uma lista como esta.

    2.1.100 [C:\Program Files\dotnet\sdk]
    2.1.101 [C:\Program Files\dotnet\sdk]
    2.1.103 [C:\Program Files\dotnet\sdk]
    2.1.104 [C:\Program Files\dotnet\sdk]
    2.1.601 [C:\Program Files\dotnet\sdk]
    2.2.101 [C:\Program Files\dotnet\sdk]
    3.0.103 [C:\Program Files\dotnet\sdk]
    6.0.200 [C:\Program Files\dotnet\sdk]
    Crie uma pasta para ser a raiz do seu projeto, onde você vai rodar o dotnet new.
    Nessa pasta, execute este comando:
    dotnet new globaljson
    O resultado será algo assim:

    {
    "sdk": {
        "version": "3.0.100-preview3-010431"
    }
    }
    Em version, substitua o 3.0.100-preview3-010431pela versão de sua preferência na --list-sdkslista. Por exemplo:
    {
    "sdk": {
        "version": "2.2.101"
    }
    }
    Corra dotnet --version para verificar. Você deveria ver:
    2.2.101
    Execute os dotnet new comandos apropriados para criar seu projeto.
    ====================================================================================
    Instalar os pacotes NuGet:

    dotnet add package Microsoft.EntityFrameworkCore.InMemory --version 3.0.1
    InMemory - É um banco de dados em Memória
    dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 3.0.1
    Aqui é pro SqlServer
