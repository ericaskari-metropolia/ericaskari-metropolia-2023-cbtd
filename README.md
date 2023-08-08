

### Migration creation
dotnet ef migrations add FirstModelAndSeeding --project CBTD --context ApplicationDbContext

### Run migrations
dotnet ef database update --project CBTD --context ApplicationDbContext
