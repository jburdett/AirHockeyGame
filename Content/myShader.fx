float4x4 WORLD;
float4x4 PROJ;
float4x4 VIEW;
float4 CAM_POS;
float4x4 WORLD_INV_TRP;
float3 VIEW_VCTR;

float4 COLOR;
float3 LGHT_PNT_POS;	// needed?
// ambient
float4 AMB_COL;
float AMB_K;
// diffuse
float4 DIFF_COL;
float DIFF_K;
// specular
float SHININESS;
float4 SPEC_COL;
float SPEC_K;

struct VS_IN
{
	float4 pos : SV_POSITION;
	float4 nrm : NORMAL;
	float4 col : COLOR;
};

struct VS_OUT
{
	float4 pos : SV_POSITION;
	float4 col : COLOR;
	float4 wpos : TEXCOORD0;
	float3 wnrm : TEXCOORD1;
};



VS_OUT VertexShaderFunction(VS_IN input)
{
	VS_OUT output;

	output.wpos = mul(input.pos, WORLD);
	output.wnrm = mul(input.nrm.xyz, (float3x3)WORLD_INV_TRP);

	// Transform vertex in world coordinates to camera coordinates
	float4 viewPosition = mul(output.wpos, VIEW);
	output.pos = mul(viewPosition, PROJ);

	// Just pass along the colour at the vertex
	output.col = input.col;

	return output;
}



float4 PixelShaderFunction(VS_OUT input) : SV_Target
{
	// Calculate ambient RGB intensities
	float3 amb = input.col.rgb * AMB_COL.rgb * AMB_K;

	// Calculate diffuse RBG reflections, we save the results of L.N because we will use it again
	float fAtt = 1;
	float3 L = normalize(LGHT_PNT_POS.xyz - input.wpos.xyz);
	float LdotN = saturate(dot(L, input.wnrm.xyz));
	float3 dif = fAtt * DIFF_COL.rgb * DIFF_K * input.col.rgb * LdotN;

	// Calculate specular reflections
	float specN = 5; // Values>>1 give tighter highlights
	float3 V = normalize(CAM_POS.xyz - input.wpos.xyz);
	float3 R = normalize(2 * LdotN * input.wnrm.xyz - L.xyz);
	//float3 R = normalize(0.5*(L.xyz+V.xyz)); //Blinn-Phong equivalent
	float3 spe = fAtt * input.col.rgb * SPEC_COL.rgb * SPEC_K * pow(saturate(dot(V, R)), specN);

	// Combine reflection components
	float4 returnCol = float4(0.0f, 0.0f, 0.0f, 0.0f);
	returnCol.rgb = amb.rgb + dif.rgb + spe.rgb;
	returnCol.a = input.col.a;

	return returnCol;
}

technique Specular
{
	pass Pass0
	{
		VertexShader = compile vs_2_0 VertexShaderFunction();
		PixelShader = compile ps_2_0 PixelShaderFunction();
	}
}