```bash
act -j azure_update_tag -W .github/workflows/character-docker.yml --container-architecture linux/amd64 --secret-file .github/workflows/.secrets --var-file .github/workflows/.vars
```