$NAME = Read-Host -Prompt 'Input migration name' 
dotnet ef migrations add $NAME --output-dir 'Migrations/DB' --context 'MyDbContext'
