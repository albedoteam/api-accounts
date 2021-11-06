// main/common variables

variable "namespace" {
  description = "Albedo Team product's namespace"
  type = string
  default = "albedoteam-products"
}

variable "do_registry_name" {
  description = "Digital Ocean registry name"
  type = string
  default = "registry.digitalocean.com/albedoteam"
}

// project variables
variable "project_secrets_name" {
  description = "Secrets name"
  type = string
  default = "accounts-api-secrets"
}

variable "project_name" {
  description = "Source name"
  type = string
  default = "accounts-api"
}

variable "project_label" {
  description = "Deployment Label / Container Name"
  type = string
  default = "AccountsApi"
}

variable "project_image_tag" {
  description = "Image tag to be pulled from registry"
  type = string
  default = "latest"
}

variable "project_replicas_count" {
  description = "Number of container replicas to provision."
  type = number
}

variable "project_service_port" {
  description = "Internal service port"
  type = number
}

variable "environment_prefix" {
  description = "Host environment to naming pattern"
  type = string
}

// project settings variables
variable "settings_broker_connection_string" {
  description = "Broker Connection String"
  type = string
  sensitive = true
  default = ""
}

variable "settings_cache_host" {
  description = "Cache Host"
  type = string
  sensitive = true
  default = ""
}

variable "settings_cache_port" {
  description = "Cache Port"
  type = number
  sensitive = true
  default = 0
}

variable "settings_cache_secret" {
  description = "Cache Secret"
  type = string
  sensitive = true
  default = ""
}

variable "settings_cache_instance_name" {
  description = "Cache Instance Name"
  type = string
  sensitive = true
  default = ""
}

variable "settings_identity_server_api_url" {
  description = "Identity Server API Url"
  type = string
  sensitive = true
  default = ""
}

variable "settings_identity_server_auth_server_id" {
  description = "Identity Server AuthServerId"
  type = string
  sensitive = true
  default = ""
}

variable "settings_identity_server_audience" {
  description = "Identity Server Audience"
  type = string
  sensitive = true
  default = ""
}

variable "settings_identity_server_allowed_origins" {
  description = "Identity Server Allowed Origins"
  type = string
  sensitive = true
  default = ""
}