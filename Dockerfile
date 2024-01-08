FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY ["RGIS_Vaja4/RGIS_Vaja4/RGIS_Vaja4.csproj", "./"]
RUN dotnet restore "RGIS_Vaja4.csproj"

COPY ["RGIS_Vaja4/RGIS_Vaja4/", "./"]
RUN dotnet build "RGIS_Vaja4.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RGIS_Vaja4.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RGIS_Vaja4.dll"]
