﻿#version 330 core

layout(location = 0) out vec4 outputColor;

in vec2 v_TextureCoord;

uniform vec4 u_Color;
uniform sampler2D u_Texture;

void main()
{
    vec4 textureColor = texture(u_Texture, v_TextureCoord) * u_Color;
    outputColor = textureColor;
}