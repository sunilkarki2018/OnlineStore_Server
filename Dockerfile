FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# COPY . .

# Copy solution file
COPY Ecommerce/*.sln .

COPY Ecommerce/Ecommerce.WebAPI/Ecommerce.WebAPI.csproj Ecommerce.WebAPI/
COPY Ecommerce/Ecommerce.Core/Ecommerce.Core.csproj Ecommerce.Core/
COPY Ecommerce/Ecommerce.Business/Ecommerce.Business.csproj Ecommerce.Business/
COPY Ecommerce/Ecommerce.Controller/Ecommerce.Controller.csproj Ecommerce.Controller/
COPY Ecommerce/Ecommerce.Test/Ecommerce.Test.csproj Ecommerce.Test/

# Restore NuGet packages
#WORKDIR /app/Ecommerce/
RUN dotnet restore

# Copy the rest of the source code
COPY Ecommerce/Ecommerce.WebAPI/ Ecommerce.WebAPI/
COPY Ecommerce/Ecommerce.Core/ Ecommerce.Core/
COPY Ecommerce/Ecommerce.Business/ Ecommerce.Business/
COPY Ecommerce/Ecommerce.Controller/ Ecommerce.Controller/
COPY Ecommerce/Ecommerce.Test/ Ecommerce.Test/

# Build and publish the API project
WORKDIR /app/Ecommerce.WebAPI
RUN dotnet publish -c Release -o /publish

# Final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "Ecommerce.WebAPI.dll"]
