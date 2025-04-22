#version 460 core
in Vec3 vPosition;
out Vec3 glPosition;

void main()
{
	gl_Position = vec4(vPosition,1.0);
	glPosition = vPostition;
}
