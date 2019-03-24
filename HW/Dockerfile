FROM microsoft/dotnet:2.2-aspnetcore-runtime-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

#FROM microsoft/dotnet:3.0.100-preview2-sdk-stretch AS build
FROM microsoft/dotnet:2.2-sdk-stretch AS build
WORKDIR /src
COPY ["**/HW.csproj", "HW/"]
RUN dotnet restore "HW/HW.csproj"
COPY . .
WORKDIR "/src/HW"
RUN dotnet build "HW.csproj" -c Debug -o /app

FROM build AS publish
RUN dotnet publish "HW.csproj" -c Debug -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "HW.dll"]
