## Get Azure Credentials

Replace items in tags as needed
```bash
az ad sp create-for-rbac --name="GitHub-ChatBox-Actions" --role="Contributor" --scopes="/subscriptions/<subscriptionid>/resourceGroups/<resourcegroupname>" --sdk-auth
```

## Run the pipeline in ACT

```bash
act -j azure_update_tag -W .github/workflows/character-docker.yml --container-architecture linux/amd64 --secret-file .github/workflows/.secrets --var-file .github/workflows/.vars
```