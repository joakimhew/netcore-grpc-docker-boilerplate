FROM microsoft/aspnetcore-build:2.0 AS build-env
WORKDIR /vsdbg
RUN apt-get update \
    && apt-get install -y --no-install-recommends \
        unzip \
    && rm -rf /var/lib/apt/lists/* \
    && curl -sSL https://aka.ms/getvsdbgsh | bash /dev/stdin -v latest -l /vsdbg 


WORKDIR /src

COPY /src/Core.Domain/Core.Domain.csproj ./src/Core.Domain/Core.Domain.csproj

COPY /src/Core.Infrastructure/Core.Infrastructure.csproj ./src/Core.Infrastructure/Core.Infrastructure.csproj

COPY /src/Services/Security/Security.API/Security.API.csproj ./src/Services/Security/Security.API/Security.API.csproj

COPY /src/Gateways/ExampleGateway/ExampleGateway.csproj ./src/Gateways/ExampleGateway/ExampleGateway.csproj

WORKDIR /src/src/Gateways/ExampleGateway
RUN dotnet restore -nowarn:msb3202,nu1503,nu1701

WORKDIR /src 
COPY . .
WORKDIR /src/src/Gateways/ExampleGateway

# Debug stage

FROM build-env AS debug
RUN dotnet publish --no-restore -c Release -o /app

WORKDIR /src

# Production stage

FROM microsoft/aspnetcore:2.0 AS prod
WORKDIR /app
COPY --from=publish /app .
EXPOSE 1112