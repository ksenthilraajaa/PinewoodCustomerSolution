# Use the official .NET Core SDK image as the base
FROM mcr.microsoft.com/dotnet/aspnet:8.0 as base
WORKDIR /app
EXPOSE 80

# Use the offiical
FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
WORKDIR /src

# Copy the project files and restore any dependencies
COPY ["PwCustomer.Api/PwCustomer.Api.csproj", "PwCustomer.Api/"]
COPY ["PwCustomer.Application/PwCustomer.Application.csproj", "PwCustomer.Application/"]
COPY ["PwCustomer.Infrastructure/PwCustomer.Infrastructure.csproj", "PwCustomer.Infrastructure/"]
#Restore all the dependencies of the
RUN dotnet restore "PwCustomer.Api/PwCustomer.Api.csproj"

# Copy the remaining files and build the app
COPY . ./
WORKDIR "/src/PwCustomer.Api"
RUN dotnet build "PwCustomer.Api.csproj" -c Release -o /app/build

#Publish the app from build stage
FROM build as Publish
RUN dotnet publish "PwCustomer.Api.csproj" -c Release -o /app/publish

#Copy the published app from publish stage output to runtime stage
FROM base as final
WORKDIR /app
COPY --from=publish /app/publish .


#Set the entry point for the container
ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS=http://0.0.0.0:5166
ENTRYPOINT [ "dotnet", "PwCustomer.Api.dll" ]