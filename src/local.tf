terraform {
  required_providers {
    kubernetes = {
      source  = "hashicorp/kubernetes"
      version = ">= 2.0.0"
    }
  }
}

provider "kubernetes" {
  config_path = "~/.kube/config"
}

resource "kubernetes_namespace" "accounts" {
  metadata {
    name = "accounts-api"
  }
}

resource "kubernetes_deployment" "accounts" {
  metadata {
    name = "accounts-api"
    namespace = kubernetes_namespace.accounts.metadata.0.name
    labels = {
      app = "AccountsApi"
    }
  }

  spec {
    replicas = 2
    selector {
      match_labels = {
        app = "accounts-api"
      }
    }
    template {
      metadata {
        labels = {
          app = "accounts-api"
        }
      }
      spec {
        container {
          image = "accounts-api:latest"
          name = "accounts-api-container"
          image_pull_policy = "IfNotPresent"
          port {
            container_port = 80
            protocol = "TCP"
          }
          env {
            name = "ASPNETCORE_URLS"
            value = "http://+:80"
          }
        }
      }
    }
  }
}

resource "kubernetes_service" "accounts" {
  metadata {
    name = "accounts-api"
    namespace = kubernetes_namespace.accounts.metadata.0.name
    labels = {
      app = "accounts-api"
    }
  }
  spec {
    type = "LoadBalancer"
    port {
      port = "5100"
      target_port = "80"
      protocol = "TCP"
    }
    selector = {
      app = kubernetes_deployment.accounts.spec.0.template.0.metadata.0.labels.app
    }
  }
}