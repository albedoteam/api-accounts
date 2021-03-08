variable "src_name" {
  description = "Source name"
  type        = string
  default     = "accounts-api"
}

variable "deployment_label" {
  description = "Deployment Label / Container Name"
  type        = string
  default     = "AccountsApi"
}

variable "secret_name" {
  description = "Secret name"
  type        = string
  default     = "accounts-secrets"
}

variable "service_port" {
  description = "Internal service port"
  type        = number
  default     = 5200
}

variable "subdomain" {
  description = "Host subdomain to expose on Ingress"
  type        = string
  default     = "accounts"
}

variable "environment-prefix" {
  description = "Host environment to expose on Ingress"
  type        = string
  default     = "rc-"
}

variable "host" {
  description = "Host suffix to expose on Ingress"
  type        = string
  default     = "albedo.team"
}

variable "broker_connection_string" {
  description = "Broker Connection String"
  type        = string
  sensitive   = true
  default     = ""
}

variable "replicas_count" {
  description = "Number of container replicas to provision."
  type        = number
  default     = 1
}

variable "container_registry" {
  description = "The name/url of the container register"
  type        = string
  default     = "registry.digitalocean.com/albedoteam-containerregistry/"
}