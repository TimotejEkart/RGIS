FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base

# delovni direktorij v sliki

WORKDIR /app

 

# vrata, ki jih želimo odpreti

EXPOSE 80

EXPOSE 443

 

# slika, ki jo uporabimo za izgradnjo

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /src

# kopiranje datotek v delovni direktorij

COPY ["RGIS_Vaja4/RGIS_Vaja4.csproj", "RGIS_Vaja4/"]



# zagon ukaza za obnovo vseh odvisnosti

RUN dotnet restore "RGIS_Vaja4/RGIS_Vaja4.csproj"

COPY . .

WORKDIR "/src/RGIS_Vaja4"

# zagon ukaza za izgradnjo projekta	

RUN dotnet build "RGIS_Vaja4.csproj" -c Release -o /app/build

 

FROM build AS publish

RUN dotnet publish "RGIS_Vaja4.csproj" -c Release -o /app/publish /p:UseAppHost=false

# zagon ukaza za objavo projekta

 

FROM base AS final

WORKDIR /app

# kopiranje datotek iz začasne slike v končno

COPY --from=publish /app/publish .

# zagon ukaza za zagon aplikacije

ENTRYPOINT ["dotnet", "RGIS_Vaja4.dll"]