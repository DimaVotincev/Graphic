#version 460 core
#define EPSILON = 0.001
#define BIG = 1000000.0
const int DIFFUSE = 1;
const int REFLECTION = 2;
const int REFRACTION = 3;

struct SSphere
{
 vec3 Center;
 float Radius;
 int MaterialIdx;
};
struct STriangle
{
 vec3 v1;
 vec3 v2;
 vec3 v3;
 int MaterialIdx;
};

out vec4 FragColor;
in vec3 glPosition;


struct SCamera
{
 vec3 Position;
 vec3 View;
 vec3 Up;
 vec3 Side;
 vec2 Scale;
};

struct SRay
{
 vec3 Origin;
 vec3 Direction;
};

SRay GenerateRay ( SCamera uCamera )
{
vec2 coords = glPosition.xy * uCamera.Scale;
vec3 direction = uCamera.View + uCamera.Side * coords.x + uCamera.Up * 
coords.y;
return SRay ( uCamera.Position, normalize(direction) );
}
SCamera initializeDefaultCamera()
{
 SCamera camera;
 camera.Position = vec3(0.0, 0.0, -8.0);
 camera.View = vec3(0.0, 0.0, 1.0);
 camera.Up = vec3(0.0, 1.0, 0.0);
 camera.Side = vec3(1.0, 0.0, 0.0);
 camera.Scale = vec2(1.0);
 return camera;
}





void initializeDefaultScene( STriangle triangles[10],  SSphere spheres[2])
{
/** TRIANGLES **/

/* left wall */
triangles[0].v1 = vec3(-5.0,-5.0,-5.0);
triangles[0].v2 = vec3(-5.0, 5.0, 5.0);
triangles[0].v3 = vec3(-5.0, 5.0,-5.0);
triangles[0].MaterialIdx = 0;
triangles[1].v1 = vec3(-5.0,-5.0,-5.0);
triangles[1].v2 = vec3(-5.0,-5.0, 5.0);
triangles[1].v3 = vec3(-5.0, 5.0, 5.0);
triangles[1].MaterialIdx = 0;

/* back wall */
triangles[2].v1 = vec3(-5.0,-5.0, 5.0);
triangles[2].v2 = vec3( 5.0,-5.0, 5.0);
triangles[2].v3 = vec3(-5.0, 5.0, 5.0);
triangles[2].MaterialIdx = 0;
triangles[3].v1 = vec3( 5.0, 5.0, 5.0);
triangles[3].v2 = vec3(-5.0, 5.0, 5.0);
triangles[3].v3 = vec3( 5.0,-5.0, 5.0);
triangles[3].MaterialIdx = 0;


/* spheres */
spheres[0].Center = vec3(-1.0,-1.0,-2.0);
spheres[0].Radius = 2.0;
spheres[0].MaterialIdx = 0;
spheres[1].Center = vec3(2.0,1.0,2.0);
spheres[1].Radius = 1.0;
spheres[1].MaterialIdx = 0;
}

STriangle triangles[10];
SSphere spheres[2];






void main ( void )
{ 
initializeDefaultScene ( triangles, spheres );
SCamera uCamera = initializeDefaultCamera();
SRay ray = GenerateRay( uCamera);
 FragColor = vec4 ( abs(ray.Direction.xy), 0, 1.0);
}
