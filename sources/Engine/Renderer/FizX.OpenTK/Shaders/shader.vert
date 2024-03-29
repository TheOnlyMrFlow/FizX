﻿#version 330 core

layout(location = 0) in vec3 position;
layout(location = 1) in vec2 textureCoord;

out vec2 v_TextureCoord;

uniform mat4 u_MVP;

void main(void)
{
    gl_Position = u_MVP * vec4(position, 1.0);
    v_TextureCoord = textureCoord;
}