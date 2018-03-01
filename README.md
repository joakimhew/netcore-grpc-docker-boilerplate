# Table of contents
1. [Prerequisites](#prerequisites)
2. [Preparation](#preparation)
3. [Getting started](#getting-started)
    1. [tl;dr version](#tldr-version)
    2. [Detailed version](#detailed-version)

## Prerequisites

| Product            | Min. version       |                     Link                                | 
| :------------------|:-------------------|:--------------------------------------------------------|
| Docker             | 17.12.0-ce (23011) | [Windows][Docker-Windows], [MacOS][Docker-macOS]        |
| Visual studio code | 1.20.1             | [Windows][VSC-Windows], [MacOS][VSC-MacOS]              |
| .NET Core SDK      | 2.0.0              | [Windows][DotnetCore-Windows], [MacOS][DotnetCore-MacOs]|

## Getting started

### tl;dr version

To start debugging all services, all you need to do is the following:
#### Shell: 
```sh
git clone git@github.com:joakimhew/netcore-grpc-docker-boilerplate.git

docker-compose -f docker-compose.yml -f docker-compose.debug.yml up --build
```

#### Visual studio code: 
    
1. Debug section -> Debug all

![vsc-debug-image][vsc-debug]


#### Code changes

Whenever you make changes to the code you do not need to rerun docker-compose. 
Just do `debug all` again and your changes will be visible. 
How this works is explained in the [detailed section](#detailed-version)


___


### Detailed version

#### Checking versions 
Confirm that you have the required version of docker by running the following command in your shell. 
**Check [prerequisites](#prerequisites)**

`docker version`

Confirm that you have the required version of Visual Studio Code. 
**Check [prerequisites](#prerequisites)**

Windows: **Help > About**

Mac OS: **Code > About Visual Studio Code**

Confirm that you have the required version of .NET Core SDK by running the following command in your shell. 
**Check [prerequisites](#prerequisites)**

`dotnet --version`

#### Preparation 
Now you're ready to start development, if you've just cloned the repository we want to make sure that our stable master branch builds and that all the docker images and containers can be created. We'll also start our containers and set them in a state ready for debugging.

#### Building all docker images
In the root of the project you'll see two compose file, **docker-compose.yml** & **docker-compose.debug.yml**.

The **docker-compose.yml** file is our base compose file that will be shared across multiple environments and the **docker-compose.debug.yml** file expands the base compose file with configuration specifically for debugging by targeting the debug stage in each dockerfile.

In your shell, navigate to the root folder where those compose files are located and run the following command: 

`docker-compose -f docker-compose.yml -f docker-compose.debug.yml build`

[docker-compose] is a tool developed by docker for running multiple [Dockerfile's][Dockerfile].

The first time you run this command, it will take some time to complete as it needs to download dotnet sdk & runtime base images.
You only need to run this command once, even if you make changes to the code.


If you take a look at the **docker-compose.debug.yml** file you'll see a section for every service called **volumes**. 

Example: 

![debug-yml-volumes-image][debug-yml-volumes]

This will make docker containers point to the output folders located on the host OS which in turn allows us to rebuild the services whenever we make changes to the code without having to rebuild the docker images. This speeds up development **A LOT**

Confirm that the images have been created by running the following command in your shell: 

`docker images`

In the list of images you should find the following images: 
1. userservice
2. cryptoservice
3. ExampleGateway
4. tokenservice



[Docker-windows]: https://docs.docker.com/docker-for-windows/install/#download-docker-for-windows
[Docker-MacOS]: https://docs.docker.com/docker-for-windows/install/#download-docker-for-windows
[VSC-Windows]: https://code.visualstudio.com/docs/setup/windows
[VSC-MacOS]: https://code.visualstudio.com/docs/setup/mac
[DotnetCore-Windows]: https://www.microsoft.com/net/download/windows
[DotnetCore-MacOS]: https://www.microsoft.com/net/download/macos
[Dockerfile]: https://docs.docker.com/engine/reference/builder/
[Docker-compose]: https://docs.docker.com/compose/

[vsc-debug]: documentation/images/vsc-debug.png
[debug-yml-volumes]: documentation/images/debug-yml-volumes.png