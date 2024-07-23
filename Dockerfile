FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 443


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
RUN ls
COPY ["e-commerce.csproj", "./"]
RUN dotnet restore e-commerce.csproj
COPY . .
RUN dotnet build e-commerce.csproj


FROM build AS publish
RUN dotnet publish "e-commerce.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "e-commerce.dll"]

