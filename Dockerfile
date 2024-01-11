FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# COPY . .

# Copy solution file
COPY Ecommerce/*.sln Ecommerce/

COPY Ecommerce/Ecommerce.WebAPI/Ecommerce.WebAPI.csproj Ecommerce/Ecommerce.WebAPI/
COPY Ecommerce/Ecommerce.Core/Ecommerce.Core.csproj Ecommerce/Ecommerce.Core/
COPY Ecommerce/Ecommerce.Business/Ecommerce.Business.csproj Ecommerce/Ecommerce.Business/
COPY Ecommerce/Ecommerce.Controller/Ecommerce.Controller.csproj Ecommerce/Ecommerce.Controller/
COPY Ecommerce/Ecommerce.Test/Ecommerce.Test.csproj Ecommerce/Ecommerce.Test/

# Restore NuGet packages
WORKDIR Ecommerce/
RUN dotnet restore

# Copy the rest of the source code
COPY Ecommerce/Ecommerce.WebAPI/ Ecommerce/Ecommerce.WebAPI/
COPY Ecommerce/Ecommerce.Core/ Ecommerce/Ecommerce.Core/
COPY Ecommerce/Ecommerce.Business/ Ecommerce/Ecommerce.Business/
COPY Ecommerce/Ecommerce.Controller/ Ecommerce/Ecommerce.Controller/
COPY Ecommerce/Ecommerce.Test/ Ecommerce/Ecommerce.Test/

# Build and publish the API project
WORKDIR /app/Ecommerce/Ecommerce.WebAPI
RUN dotnet publish -c Release -o /publish

# Final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/Ecommerce/Ecommerce.WebAPI/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "Ecommerce.WebAPI.dll"]
