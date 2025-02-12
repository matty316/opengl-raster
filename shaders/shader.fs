#version 460 core
struct Material {
	sampler2D diffuse;
	vec3 specular;
	float shininess;
};

uniform Material material;

struct Light {
	vec3 position;

	vec3 ambient;
	vec3 diffuse;
	vec3 specular;
};
uniform Light light;

uniform vec3 viewPos;

in vec3 Normal;
in vec3 FragPos;
in vec2 TexCoords;

out vec4 FragColor;

void main() {
  vec3 ambient = material.ambient * light.ambient;

  vec3 norm = normalize(Normal);
  vec3 lightDir = normalize(light.position - FragPos);

  float diff = max(dot(norm, lightDir), 0.0);
  vec3 diffuse = (diff * material.diffuse) * light.diffuse;

  vec3 viewDir = normalize(viewPos - FragPos);
  vec3 reflectDir = reflect(-lightDir, norm);
  float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);
  vec3 specular = (material.specular * spec) * light.specular;

  vec3 result = ambient + diffuse + specular;

  FragColor = vec4(result, 1.0);
}
