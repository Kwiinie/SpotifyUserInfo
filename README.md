# Main Features:
- View user's profile
- View top tracks/artist
- View recently played tracks
- View user's playlists and saved tracks
- Recommend tracks based on user's top items
- Recommend artist based on user's top artist
- Add to playlist or saved recommended tracks

# Docker commands:
**Docker pull**
```bash
docker pull kwiinie/spotifyuser:latest
```

**Docker run (Windows-bash)**
```bash
docker run -d \
  -p 5165:8080 \
  -p 7121:8081 \
  -e ASPNETCORE_ENVIRONMENT=Development \
  -e ASPNETCORE_HTTP_PORTS=8080 \
  -e ASPNETCORE_HTTPS_PORTS=8081 \
  -v "${APPDATA}/Microsoft/UserSecrets:/home/app/.microsoft/usersecrets:ro" \
  -v "${APPDATA}/ASP.NET/Https:/home/app/.aspnet/https:ro" \
  kwiinie/spotifyuser:latest
```


