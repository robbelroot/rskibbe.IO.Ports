build
  dotnet build
create nuget package
  dotnet pack
publish nuget package
  cd <folder containing nuget file>
  dotnet nuget push <nuget package filename> --api-key <apikey>