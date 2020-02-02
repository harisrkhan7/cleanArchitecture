DOTNET = dotnet

remake_database: nuke_database make_database

nuke_database:
	$(DOTNET) restore -s cleanArchitecture.Web/
	$(DOTNET) ef database drop -f -s cleanArchitecture.Web/

make_database:
	$(DOTNET) restore -s cleanArchitecture.Web/
	$(DOTNET) ef database update -s cleanArchitecture.Web/
