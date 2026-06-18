FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

COPY ["Legacy.Profile.Api/Legacy.Profile.Api.csproj", "Legacy.Profile.Api/"]
COPY ["Legacy.Profile.Application/Legacy.Profile.Application.csproj", "Legacy.Profile.Application/"]
COPY ["Legacy.Profile.Domain/Legacy.Profile.Domain.csproj", "Legacy.Profile.Domain/"]

RUN dotnet restore "Legacy.Profile.Api/Legacy.Profile.Api.csproj"

COPY . .

WORKDIR "/src/Legacy.Profile.Api"
RUN dotnet publish "Legacy.Profile.Api.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime

ENTRYPOINT ["dotnet", "Legacy.Profile.Api.dll"]