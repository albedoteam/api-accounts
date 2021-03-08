terraform {
  required_providers {
    kubernetes = {
      source  = "hashicorp/kubernetes"
      version = ">= 2.0.0"
    }
    digitalocean = {
      source  = "digitalocean/digitalocean"
      version = "2.5.1"
    }
  }
  backend "kubernetes" {
    secret_suffix    = "accounts-api"
    load_config_file = true
  }
}

provider "kubernetes" {
  config_path = "~/.kube/config"
}

provider "digitalocean" {
  token = "d2bae1fd035a2aef0309a3414bda20dff89d1283b580000f4387c0d165a218e3"
}

resource "digitalocean_container_registry_docker_credentials" "do_container_registry" {
  registry_name = "registry.digitalocean.com/albedoteam-containerregistry"
}

resource "kubernetes_namespace" "accounts" {
  metadata {
    name = var.src_name
  }
}

resource "kubernetes_secret" "do_cr_secret" {
  metadata {
    name      = "${var.src_name}-do-registry"
    namespace = kubernetes_namespace.accounts.metadata.0.name
  }
  data = {
    ".dockerconfigjson" = digitalocean_container_registry_docker_credentials.do_container_registry.docker_credentials
  }
  type = "kubernetes.io/dockerconfigjson"
}

resource "kubernetes_secret" "accounts" {
  metadata {
    name      = var.secret_name
    namespace = kubernetes_namespace.accounts.metadata.0.name
  }
  data = {
    Broker_Host = var.broker_connection_string
  }
}

resource "kubernetes_deployment" "accounts" {
  metadata {
    name      = var.src_name
    namespace = kubernetes_namespace.accounts.metadata.0.name
    labels = {
      app = var.deployment_label
    }
  }

  spec {
    replicas = var.replicas_count
    selector {
      match_labels = {
        app = var.src_name
      }
    }
    template {
      metadata {
        labels = {
          app = var.src_name
        }
      }
      spec {
        image_pull_secrets {
          name = "${var.src_name}-do-registry"
        }
        container {
          image             = "${var.container_registry}${var.src_name}:latest"
          name              = "${var.src_name}-container"
          image_pull_policy = "IfNotPresent"
          resources {
            limits = {
              cpu    = "0.5"
              memory = "512Mi"
            }
            requests = {
              cpu    = "250m"
              memory = "50Mi"
            }
          }
          port {
            container_port = 80
            protocol       = "TCP"
          }
          env {
            name  = "ASPNETCORE_URLS"
            value = "http://+:80"
          }
          env_from {
            secret_ref {
              name = var.secret_name
            }
          }
        }
      }
    }
  }
}

resource "kubernetes_service" "accounts" {
  metadata {
    name      = var.src_name
    namespace = kubernetes_namespace.accounts.metadata.0.name
    labels = {
      app = var.src_name
    }
  }
  spec {
    type = "LoadBalancer"
    port {
      port        = var.service_port
      target_port = "80"
      protocol    = "TCP"
    }
    selector = {
      app = kubernetes_deployment.accounts.spec.0.template.0.metadata.0.labels.app
    }
  }
}

resource "kubernetes_ingress" "accounts" {
  metadata {
    name      = var.src_name
    namespace = kubernetes_namespace.accounts.metadata.0.name
    labels = {
      app = var.src_name
    }
  }
  spec {
    backend {
      service_name = var.src_name
      service_port = var.service_port
    }
    rule {
      host = "${var.environment-prefix}${var.subdomain}.${var.host}"
      http {

        path {
          path = "/"
          backend {
            service_name = var.src_name
            service_port = var.service_port
          }
        }
      }
    }
  }
}