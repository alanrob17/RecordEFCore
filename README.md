# RecordEFCore

Build.

```bash
	docker build -t recordefcore .
```

Run.

```bash
	docker run --name record-app -p 8080:8080 -e DOTNET_URLS=http://+:8080 recordefcore:latest
```