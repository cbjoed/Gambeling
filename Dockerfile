# Use the official .NET 8 image as the base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Use the SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["GamblingBot/GamblingBot.csproj", "GamblingBot/"]
RUN dotnet restore "GamblingBot/GamblingBot.csproj"
COPY . .
WORKDIR "/src/GamblingBot"
RUN dotnet build "GamblingBot.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GamblingBot.csproj" -c Release -o /app/publish

# Final image setup
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GamblingBot.dll"]