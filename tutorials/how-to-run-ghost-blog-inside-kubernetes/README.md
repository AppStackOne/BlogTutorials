# How to run Ghost blog inside Kubernetes

Example to follow with :

https://blog.appstack.one/how-to-run-ghost-blog-inside-kubernetes/

Execute thoses lines to deploy a Ghost blog!
```
kubectl create namespace blog
kubectl apply -f .\blog-persistent-volume.yml --namespace=blog
kubectl apply -f .\blog-persistent-volume-claim.yml --namespace=blog
kubectl apply -f .\blog-deployment.yml --namespace=blog
kubectl apply -f .\blog-service.yml --namespace=blog
kubectl apply -f .\blog-tls.yml --validate=false --namespace=blog
kubectl apply -f .\blog-ingress.yml --namespace=blog
```