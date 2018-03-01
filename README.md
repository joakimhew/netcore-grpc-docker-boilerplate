![build-status][build-status]


# Table of contents
1. [Project structure](#project-structure)
2. [Prerequisites](#prerequisites)
3. [Tools used](#tools-used)
4. [Preparing](#preparing)
5. [Running](#getting-started)
    1. [Visual Studio Code](#visual-studio-code)
    2. [Visual Studio](#detailed-version)
6. [Modifying grpc proto files](#modifying-grpc-proto-files)


## Project structure

## Prerequisites

| Product            | Min. version       |                     Link                                | 
| :------------------|:-------------------|:--------------------------------------------------------|
| Docker             | 17.12.0-ce (23011) | [Windows][Docker-Windows], [MacOS][Docker-macOS]        |
| Visual studio code | 1.20.1             | [Windows][VSC-Windows], [MacOS][VSC-MacOS]              |
| .NET Core SDK      | 2.0.0              | [Windows][DotnetCore-Windows], [MacOS][DotnetCore-MacOs]|


## Tools used

| Tool                             | 
| :--------------------------------|
| [docker-compose][docker-compose] |
| [grpc][grpc]                     |


## Preparing
```sh
git clone git@github.com:joakimhew/netcore-grpc-docker-boilerplate.git

docker-compose -f docker-compose.yml -f docker-compose.debug.yml up --build -d
```
## Running

### Visual Studio Code

1. Debug section -> Debug all

![vsc-debug-image][vsc-debug]

___


[Docker-windows]: https://docs.docker.com/docker-for-windows/install/#download-docker-for-windows
[Docker-MacOS]: https://docs.docker.com/docker-for-windows/install/#download-docker-for-windows
[VSC-Windows]: https://code.visualstudio.com/docs/setup/windows
[VSC-MacOS]: https://code.visualstudio.com/docs/setup/mac
[DotnetCore-Windows]: https://www.microsoft.com/net/download/windows
[DotnetCore-MacOS]: https://www.microsoft.com/net/download/macos
[Dockerfile]: https://docs.docker.com/engine/reference/builder/

[docker-compose]: https://docs.docker.com/compose/
[grpc]: https://grpc.io/


[vsc-debug]: documentation/images/vsc-debug.png
[debug-yml-volumes]: documentation/images/debug-yml-volumes.png

[build-status]: https://travis-ci.org/joakimhew/netcore-grpc-docker-boilerplate.svg?branch=master