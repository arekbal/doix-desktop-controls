in vec3 Pos;
in vec4 Color;
in vec2 TexCoord;

varying vec4 var_color;

void main()
{
    gl_TexCoord[0] = vec4(TexCoord.x, TexCoord.y, 0, 0);//gl_MultiTexCoord0;
    gl_Position = ftransform();
	//gl_Position = gl_Vertex;
	//gl_FrontColor = Color;

	var_color = Color;
}