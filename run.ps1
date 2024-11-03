

Start-Process -FilePath "cmd" -ArgumentList "/c", "cd ./Dashboard/Dashboard.Server && dotnet run"

Start-Process -FilePath "cmd" -ArgumentList "/c", "cd ./SagaCoordinator && dotnet run"

Start-Process -FilePath "cmd" -ArgumentList "/c", "cd ./TriangulatorWorker && dotnet run"
Start-Process -FilePath "cmd" -ArgumentList "/c", "cd ./StructureGeneratorWorker && dotnet run"
Start-Process -FilePath "cmd" -ArgumentList "/c", "cd ./FacadeGeneratorWorker && dotnet run"
Start-Process -FilePath "cmd" -ArgumentList "/c", "cd ./CalculixInputGeneratorWorker && dotnet run"