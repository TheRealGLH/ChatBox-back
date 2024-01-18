
## K3D

### Set up cluster and network

Creating the local cluster:

```bash
k3d cluster create chatbox --servers 1 --agents 1 --port 9080:80@loadbalancer --registry-config k3d-registry-config.yml
```

```bash
k3d cluster list
```

```bash
k3d cluster delete <name>/--all
```

check if running
```bash
kubectl get nodes
```

### Put in the yamls

To deploy the image to our kubernetes cluster we need the following command.
```bash
kubectl apply -f *-deployment.yaml
```

TODO: write how the port number in the deployment file is for the container, not the app hosted inside of it.

Delete deployment
```bash
kubectl delete deployment [name]
```
To check if a deployment was succesfull we can run.
```bash
kubectl get deployment
```

If we want the deployment and its pods to be accessed on the network, we need to enable a service for it.
```bash
kubectl apply -f *-service.yaml
```


> apply ingress


## Configuration

Example from gateway, to make config based on existing config files that Kubernetes pods use:

- pass multiple files if you want, use ``--from-file=<path>``
- ``-o`` prints the output to console, can choose the output type (like json or yaml)
- ``--dry-run=client`` is used to not contact kubernetes cluster and save it. We just want the file to manually apply

```bash
kubectl create configmap gateway-config --from-file=../../Gateway/appsettings.json --from-file=../../Gateway/ocelot.json -o yaml --dry-run=client > gateway-configmap.yml
```

TODO: how do we apply this output to a pod?

## Port forward pod
To check whenever we can reach the api we can use the `kubectl port-forward` command.

```bash
kubectl port-forward [POD_NAME] <external-port>:<application:port>
```


## Rabbit Stuff
https://www.rabbitmq.com/kubernetes/operator/quickstart-operator.html

## AKS

https://learn.microsoft.com/en-us/azure/aks/learn/quick-kubernetes-deploy-cli


create resource group
```bash
az group create --name ChatboxKubeResourcegroup --location westeurope
```
create cluster
```bash
az aks create --resource-group ChatboxKubeResourcegroup --name ChatboxAKSCluster --enable-managed-identity --node-count 1 --enable-addons monitoring --enable-msi-auth-for-monitoring --generate-ssh-keys
```

connect to cluster
```bash
az aks get-credentials --resource-group ChatboxKubeResourcegroup --name ChatboxAKSCluster
```

clone k8s files
```bash
git clone git@github.com:TheRealGLH/Chatbox-k8s.git
```

### get public IP for service

https://learn.microsoft.com/en-us/azure/aks/static-ip

get node resource group name
```bash
az aks show --name ChatboxAKSCluster --resource-group ChatboxKubeResourcegroup --query nodeResourceGroup -o tsv
```

will give output like ``MC_ChatboxKubeResourcegroup_ChatboxAKSCluster_westeurope``

```bash
az network public-ip create --resource-group MC_ChatboxKubeResourcegroup_ChatboxAKSCluster_westeurope --name ChatboxPublicIP --sku Standard --allocation-method static
```
replace resource_group value with output from previous

```bash
az network public-ip show --resource-group MC_ChatboxKubeResourcegroup_ChatboxAKSCluster_westeurope --name ChatboxPublicIP --query ipAddress --output tsv
```

set permissions
```bash
CLIENT_ID=$(az aks show --name ChatboxAKSCluster --resource-group ChatboxKubeResourcegroup --query identity.principalId -o tsv)
RG_SCOPE=$(az group show --name MC_ChatboxKubeResourcegroup_ChatboxAKSCluster_westeurope --query id -o tsv)
az role assignment create --assignee ${CLIENT_ID} --role "Network Contributor" --scope ${RG_SCOPE}
```


Order of deployments:

- RabbitMQ
- Wait for Deployments
- Microservices


### Delete when done working

```bash
az group delete --name ChatboxKubeResourcegroup --yes --no-wait
```
```bash
az group delete --name MC_ChatboxKubeResourcegroup_ChatboxAKSCluster_westeurope --yes --no-wait
```

### Azure credentials
```bash
az ad sp create-for-rbac --name="GitHub-ChatBox-Actions" --role="Contributor" --scopes="/subscriptions/cafa1880-3da7-4d98-8bfe-2a4c8b11ddf8/resourceGroups/ChatboxKubeResourcegroup" --sdk-auth
```