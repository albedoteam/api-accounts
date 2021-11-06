terraform {
  required_providers {
    kubernetes = {
      source = "hashicorp/kubernetes"
      version = ">= 2.0.0"
    }
  }
  backend "kubernetes" {}
}

provider "kubernetes" {
  config_path = "~/.kube/config"
}

resource "kubernetes_secret" "accounts" {
  metadata {
    name = "${var.environment_prefix}${var.project_secrets_name}"
    namespace = var.namespace
  }
  data = {
    Broker_Host = var.settings_broker_connection_string
    Cache_Host = var.settings_cache_host
    Cache_Port = var.settings_cache_port
    Cache_Secret = var.settings_cache_secret
    Cache_InstanceName = var.settings_cache_instance_name
    IdentityServer_ApiUrl = var.settings_identity_server_api_url
    IdentityServer_AuthServerId = var.settings_identity_server_auth_server_id
    IdentityServer_Audience = var.settings_identity_server_audience
    IdentityServer_AllowedOrigins = var.settings_identity_server_allowed_origins
  }
}

resource "kubernetes_deployment" "accounts" {
  metadata {
    name = "${var.environment_prefix}${var.project_name}"
    namespace = var.namespace
    labels = {
      app = "${var.environment_prefix}${var.project_label}"
    }
  }

  spec {
    replicas = var.project_replicas_count
    selector {
      match_labels = {
        app = "${var.environment_prefix}${var.project_name}"
      }
    }
    template {
      metadata {
        labels = {
          app = "${var.environment_prefix}${var.project_name}"
        }
      }
      spec {
        image_pull_secrets {
          name = "${var.namespace}-do-registry"
        }
        container {
          image = "${var.do_registry_name}/${var.project_name}:${var.project_image_tag}"
          name = "${var.environment_prefix}${var.project_name}-container"
          image_pull_policy = "Always"
          resources {
            limits = {
              cpu = "150m"
              memory = "150Mi"
            }
            requests = {
              cpu = "50m"
              memory = "50Mi"
            }
          }
          port {
            container_port = 80
            protocol = "TCP"
          }
          env {
            name = "ASPNETCORE_URLS"
            value = "http://+:80"
          }
          env_from {
            secret_ref {
              name = "${var.environment_prefix}${var.project_secrets_name}"
            }
          }
        }
      }
    }
  }
}

resource "kubernetes_service" "accounts" {
  metadata {
    name = "${var.environment_prefix}${var.project_name}"
    namespace = var.namespace
    labels = {
      app = "${var.environment_prefix}${var.project_name}"
    }
  }
  spec {
    type = "ClusterIP"
    port {
      name = "http"
      port = var.project_service_port
      target_port = 80
      protocol = "TCP"
    }
    selector = {
      app = kubernetes_deployment.accounts.spec.0.template.0.metadata.0.labels.app
    }
  }
}