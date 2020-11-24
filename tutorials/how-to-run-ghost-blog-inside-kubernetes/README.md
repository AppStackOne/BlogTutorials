# How to run Ghost blog inside Kubernetes

Follow with :

https://blog.appstack.one/how-to-run-ghost-blog-inside-kubernetes/

![Ghost](https://ghost.org/images/logo.svg)

Execute thoses lines to deploy!
```powershell
kubectl create namespace blog
kubectl apply -f .\blog-persistent-volume.yml --namespace=blog
kubectl apply -f .\blog-persistent-volume-claim.yml --namespace=blog
kubectl apply -f .\blog-deployment.yml --namespace=blog
kubectl apply -f .\blog-service.yml --namespace=blog
kubectl apply -f .\blog-tls.yml --validate=false --namespace=blog
kubectl apply -f .\blog-ingress.yml --namespace=blog
```