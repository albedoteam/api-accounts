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
    name = var.src_name
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
        container {
          image             = "${var.src_name}:latest"
          name              = "${var.src_name}-container"
          image_pull_policy = "IfNotPresent"
          args              = ["Broker:Host=${var.broker_connection_string}"]
          port {
            container_port = 80
            protocol       = "TCP"
          }
          env {
            name  = "ASPNETCORE_URLS"
            value = "http://+:80"
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
      port        = "5100"
      target_port = "80"
      protocol    = "TCP"
    }
    selector = {
      app = kubernetes_deployment.accounts.spec.0.template.0.metadata.0.labels.app
    }
  }
}