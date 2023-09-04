﻿//////////////////////////////////////////////////////
// MK Toon Editor Particles Component				//
//					                                //
// Created by Michael Kremmel                       //
// www.michaelkremmel.de                            //
// Copyright © 2020 All rights reserved.            //
//////////////////////////////////////////////////////

#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using System;
using UnityEditor.Utils;
using UnityEditorInternal;
using EditorHelper = MK.Toon.Editor.EditorHelper;

namespace MK.Toon.Editor
{
    internal sealed class ParticlesComponent : ShaderGUI
    {   
        internal bool active { get { return _particlesBehavior != null; } }

        /////////////////////////////////////////////////////////////////////////////////////////////
		// Properties                                                                              //
		/////////////////////////////////////////////////////////////////////////////////////////////
        //Unity source based particle properties
        private static ReorderableList vertexStreamList;
        List<ParticleSystemRenderer> m_RenderersUsingThisMaterial = new List<ParticleSystemRenderer>();

        /////////////////
        // Particles   //
        /////////////////
        internal MaterialProperty _flipbook;
        internal MaterialProperty _softFade;
        internal MaterialProperty _softFadeNearDistance;
        internal MaterialProperty _softFadeFarDistance;
        internal MaterialProperty _cameraFade;
        internal MaterialProperty _cameraFadeNearDistance;
        internal MaterialProperty _cameraFadeFarDistance;
        internal MaterialProperty _colorBlend;

        private MaterialProperty _particlesBehavior;

        internal void FindProperties(MaterialProperty[] props)
        {
            _flipbook = FindProperty(Properties.flipbook.uniform.name, props, false);
            _softFade = FindProperty(Properties.softFade.uniform.name, props, false);
            _softFadeNearDistance = FindProperty(Properties.softFadeNearDistance.uniform.name, props, false);
            _softFadeFarDistance = FindProperty(Properties.softFadeFarDistance.uniform.name, props, false);
            _cameraFade = FindProperty(Properties.cameraFade.uniform.name, props, false);
            _cameraFadeNearDistance = FindProperty(Properties.cameraFadeNearDistance.uniform.name, props, false);
            _cameraFadeFarDistance = FindProperty(Properties.cameraFadeFarDistance.uniform.name, props, false);
            _colorBlend = FindProperty(Properties.colorBlend.uniform.name, props, false);

            _particlesBehavior = FindProperty(EditorProperties.particlesTab.uniform.name, props, false);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
		// Setup                                                                                   //
		/////////////////////////////////////////////////////////////////////////////////////////////
        
        /// <summary>
        /// Find similar values from the changed shader
        /// </summary>
        /// <param name="propertiesSrc"></param>
        /// <param name="materialDst"></param>
        /// <param name="materialSrc"></param>
        internal void ConvertSimilarValues(MaterialProperty[] propertiesSrc, Material materialSrc, Material materialDst)
        {
            MaterialProperty flipbook = FindProperty("_Flipbook", propertiesSrc, false);
            MaterialProperty flipbookBlending = FindProperty("_FlipbookBlending", propertiesSrc, false);
            MaterialProperty softParticlesEnabled = FindProperty("_SoftParticlesEnabled", propertiesSrc, false);
            MaterialProperty softParticlesNearFadeDistance = FindProperty("_SoftParticlesNearFadeDistance", propertiesSrc, false);
            MaterialProperty softParticlesFarFadeDistance = FindProperty("_SoftParticlesFarFadeDistance", propertiesSrc, false);
            MaterialProperty cameraFadingEnabled = FindProperty("_CameraFadingEnabled", propertiesSrc, false);
            MaterialProperty cameraNearFadeDistance = FindProperty("_CameraNearFadeDistance", propertiesSrc, false);
            MaterialProperty cameraFarFadeDistance = FindProperty("_CameraFarFadeDistance", propertiesSrc, false);
            MaterialProperty colorBlend = FindProperty("_ColorMode", propertiesSrc, false);

            if(flipbook != null)
                Properties.flipbook.SetValue(materialDst, flipbook.floatValue > 0 ? true : false);
            if(flipbookBlending != null)
                Properties.flipbook.SetValue(materialDst, flipbookBlending.floatValue > 0 ? true : false);
            if(softParticlesEnabled != null)
                Properties.softFade.SetValue(materialDst, softParticlesEnabled.floatValue > 0 ? true : false);
            if(softParticlesNearFadeDistance != null)
                Properties.softFadeNearDistance.SetValue(materialDst, softParticlesNearFadeDistance.floatValue);
            if(softParticlesFarFadeDistance != null)
                Properties.softFadeFarDistance.SetValue(materialDst, softParticlesFarFadeDistance.floatValue);
            if(cameraFadingEnabled != null)
                Properties.cameraFade.SetValue(materialDst, cameraFadingEnabled.floatValue > 0 ? true : false);
            if(cameraNearFadeDistance != null)
                Properties.cameraFadeNearDistance.SetValue(materialDst, cameraNearFadeDistance.floatValue);
            if(cameraFarFadeDistance != null)
                Properties.cameraFadeFarDistance.SetValue(materialDst, cameraFarFadeDistance.floatValue);
            if(colorBlend != null)
                Properties.colorBlend.SetValue(materialDst, (ColorBlend) colorBlend.floatValue);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
		// Draw                                                                                    //
		/////////////////////////////////////////////////////////////////////////////////////////////

        internal void Initialize(MaterialEditor materialEditor) 
        {
            CacheRenderersUsingThisMaterial(materialEditor);
        }
        
        //Cache function based on unity source
        private void CacheRenderersUsingThisMaterial(MaterialEditor materialEditor)
        {
            m_RenderersUsingThisMaterial.Clear();

            Material material = materialEditor.target as Material;

            foreach (var obj in  materialEditor.targets)
            {
                ParticleSystemRenderer[] renderers = UnityEngine.Object.FindObjectsOfType(typeof(ParticleSystemRenderer)) as ParticleSystemRenderer[];
                foreach (ParticleSystemRenderer renderer in renderers)
                {
                    if (renderer.sharedMaterial == material)
                        m_RenderersUsingThisMaterial.Add(renderer);
                }
            }
        }

        internal void DrawParticles(MaterialEditor materialEditor, MaterialProperty[] properties, MaterialProperty surface, ShaderTemplate shaderTemplate)
        {
            //All refraction properties needs to be available on the material
            //the refraction tab is used for check
            if(_particlesBehavior != null)
            {
                if(EditorHelper.HandleBehavior(UI.particlesTab.text, "", _particlesBehavior, null, materialEditor, false))
                {
                    DrawColorBlend(materialEditor);
                    DrawFlipbookMode(materialEditor);
                    DrawSoftFadeMode(materialEditor, (Surface) surface.floatValue);
                    DrawCameraFadeMode(materialEditor, (Surface) surface.floatValue);
                    DrawVertexDataStream(materialEditor, shaderTemplate);
                }
                EditorHelper.DrawSplitter();
            }
        }
        /////////////////
        // Options     //
        /////////////////
        internal void DrawColorBlend(MaterialEditor materialEditor)
        {
            EditorGUI.BeginChangeCheck();
            materialEditor.ShaderProperty(_colorBlend, UI.colorBlend);
            if(EditorGUI.EndChangeCheck())
            {
                ManageKeywordsColorBlend();
            }
        }

        /////////////////
        // Advanced    //
        /////////////////
        private static void VertexStreamsArea(Material material, List<ParticleSystemRenderer> renderers, ShaderTemplate shaderTemplate)
        {
            bool useLighting, isPBS;
            useLighting = shaderTemplate != ShaderTemplate.Unlit ? true : false;
            isPBS = shaderTemplate == ShaderTemplate.PhysicallyBased ? true : false;
            EditorGUILayout.Space();
            // Display list of streams required to make this shader work
            bool useNormalMap = false;
            bool useFlipbookBlending = Properties.flipbook.GetValue(material);
            if(useLighting)
                useNormalMap = Properties.normalMap.GetValue(material) != null;
            bool useHeightMap = false;
            if(isPBS)
                useHeightMap = Properties.heightMap.GetValue(material) != null;
            bool useDetailNormalMap = false;
            if(isPBS)
                useDetailNormalMap = Properties.detailNormalMap.GetValue(material) != null && isPBS;
            bool useAnisotropicSpecular = false;
            if(useLighting)
                useAnisotropicSpecular = Properties.specular.GetValue(material) != Specular.Anisotropic && isPBS;

            // Build the list of expected vertex streams
            List<ParticleSystemVertexStream> streams = new List<ParticleSystemVertexStream>();
            List<string> streamList = new List<string>();

            streams.Add(ParticleSystemVertexStream.Position);
            streamList.Add(EditorHelper.EditorStyles.streamPositionText);

            if (useLighting)
            {
                streams.Add(ParticleSystemVertexStream.Normal);
                streamList.Add(EditorHelper.EditorStyles.streamNormalText);
                if (useNormalMap || useDetailNormalMap || useDetailNormalMap || useHeightMap)
                {
                    streams.Add(ParticleSystemVertexStream.Tangent);
                    streamList.Add(EditorHelper.EditorStyles.streamTangentText);
                }
            }

            streams.Add(ParticleSystemVertexStream.Color);
            streamList.Add(EditorHelper.EditorStyles.streamColorText);
            streams.Add(ParticleSystemVertexStream.UV);
            streamList.Add(EditorHelper.EditorStyles.streamUVText);

            if (useFlipbookBlending)
            {
                streams.Add(ParticleSystemVertexStream.UV2);
                streamList.Add(EditorHelper.EditorStyles.streamUV2Text);
                streams.Add(ParticleSystemVertexStream.AnimBlend);
                streamList.Add(EditorHelper.EditorStyles.streamAnimBlendText);
            }

            vertexStreamList = new ReorderableList(streamList, typeof(string), false, true, false, false);

            vertexStreamList.drawHeaderCallback = (Rect rect) => {
                EditorGUI.LabelField(rect, "Vertex Streams");
            };

            vertexStreamList.DoLayoutList();

            // Display a warning if any renderers have incorrect vertex streams
            string Warnings = "";
            List<ParticleSystemVertexStream> rendererStreams = new List<ParticleSystemVertexStream>();
            foreach (ParticleSystemRenderer renderer in renderers)
            {
                renderer.GetActiveVertexStreams(rendererStreams);
                if (!rendererStreams.SequenceEqual(streams))
                    Warnings += "-" + renderer.name + "\n";
            }

            if (!string.IsNullOrEmpty(Warnings))
            {
                EditorGUILayout.HelpBox(
                    "The following Particle System Renderers are using this material with incorrect Vertex Streams:\n" +
                    Warnings, MessageType.Error, true);
                // Set the streams on all systems using this material
                if (GUILayout.Button(EditorHelper.EditorStyles.streamApplyToAllSystemsText, EditorStyles.miniButton, GUILayout.ExpandWidth(true)))
                {
                    Undo.RecordObjects(renderers.Where(r => r != null).ToArray(), EditorHelper.EditorStyles.undoApplyCustomVertexStreams);

                    foreach (ParticleSystemRenderer renderer in renderers)
                    {
                        renderer.SetActiveVertexStreams(streams);
                    }
                }
            }
        }

        internal void DrawFlipbookMode(MaterialEditor materialEditor)
        {
            EditorGUI.BeginChangeCheck();
            materialEditor.ShaderProperty(_flipbook, UI.flipbook);
            if(EditorGUI.EndChangeCheck())
            {
                ManageKeywordsFlipbook();
            }
        }

        internal void DrawSoftFadeMode(MaterialEditor materialEditor, Surface surface)
        {
            EditorGUI.BeginChangeCheck();
            if(surface == Surface.Transparent)
            {
                materialEditor.ShaderProperty(_softFade, UI.softFade);
                if(_softFade.floatValue > 0)
                {
                    materialEditor.ShaderProperty(_softFadeNearDistance, UI.softFadeNearDistance);
                    materialEditor.ShaderProperty(_softFadeFarDistance, UI.softFadeFarDistance);
                }
            }
            if(EditorGUI.EndChangeCheck())
            {
                ManageKeywordsSoftFade();
            }
        }        

        internal void DrawCameraFadeMode(MaterialEditor materialEditor, Surface surface)
        {
            EditorGUI.BeginChangeCheck();
            if(surface == Surface.Transparent)
            {
                materialEditor.ShaderProperty(_cameraFade, UI.cameraFade);
                if(_cameraFade.floatValue > 0)
                {
                    materialEditor.ShaderProperty(_cameraFadeNearDistance, UI.cameraFadeNearDistance);
                    materialEditor.ShaderProperty(_cameraFadeFarDistance, UI.cameraFadeFarDistance);
                }
            }
            if(EditorGUI.EndChangeCheck())
            {
                ManageKeywordsCameraFade();
            }
        }   

        internal void DrawVertexDataStream(MaterialEditor materialEditor, ShaderTemplate shaderTemplate)
        {
            EditorHelper.VerticalSpace();
            EditorGUI.BeginChangeCheck();
            {
                foreach (Material mat in _colorBlend.targets)
                {
                    VertexStreamsArea(mat, m_RenderersUsingThisMaterial, shaderTemplate);
                }
            }
            EditorGUI.EndChangeCheck();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
		// Variants Setup                                                                          //
		/////////////////////////////////////////////////////////////////////////////////////////////
        internal void ManageKeywordsColorBlend()
        {
            if(_particlesBehavior != null)
            {
                //Color Mode
                foreach (Material mat in _colorBlend.targets)
                {
                    ColorBlend cm = Properties.colorBlend.GetValue(mat);
                    EditorHelper.SetKeyword(Properties.surface.GetValue(mat) == Surface.Transparent && Properties.colorBlend.GetValue(mat) == ColorBlend.Additive, Keywords.colorBlend[1], mat);
                    EditorHelper.SetKeyword(Properties.surface.GetValue(mat) == Surface.Transparent && Properties.colorBlend.GetValue(mat) == ColorBlend.Subtractive, Keywords.colorBlend[2], mat);
                    EditorHelper.SetKeyword(Properties.surface.GetValue(mat) == Surface.Transparent && Properties.colorBlend.GetValue(mat) == ColorBlend.Overlay, Keywords.colorBlend[3], mat);
                    EditorHelper.SetKeyword(Properties.surface.GetValue(mat) == Surface.Transparent && Properties.colorBlend.GetValue(mat) == ColorBlend.Color, Keywords.colorBlend[4], mat);
                    EditorHelper.SetKeyword(Properties.surface.GetValue(mat) == Surface.Transparent && Properties.colorBlend.GetValue(mat) == ColorBlend.Difference, Keywords.colorBlend[5], mat);
                    //No Keyword == Multiply

                    Properties.colorBlend.SetValue(mat, cm);
                }
            }
        }

        internal void ManageKeywordsFlipbook()
        {
            if(_particlesBehavior != null)
            {
                //Flipbook Mode
                foreach (Material mat in _flipbook.targets)
                {
                    EditorHelper.SetKeyword(Properties.flipbook.GetValue(mat), Keywords.flipbook, mat);
                }
            }
        }

        internal void ManageKeywordsSoftFade()
        {
            if(_particlesBehavior != null)
            {
                //Soft Fade Mode
                foreach (Material mat in _softFade.targets)
                {
                    EditorHelper.SetKeyword(Properties.surface.GetValue(mat) == Surface.Transparent && Properties.softFade.GetValue(mat), Keywords.softFade, mat);
                }
            }
        }

        internal void ManageKeywordsCameraFade()
        {
            if(_particlesBehavior != null)
            {
                //Camera Fade Mode
                foreach (Material mat in _cameraFade.targets)
                {
                    EditorHelper.SetKeyword(Properties.surface.GetValue(mat) == Surface.Transparent && Properties.cameraFade.GetValue(mat), Keywords.cameraFade, mat);
                }
            }
        }

       internal void UpdateKeywords()
        {
            ManageKeywordsColorBlend();
            ManageKeywordsFlipbook();
            ManageKeywordsSoftFade();
            ManageKeywordsCameraFade();
        }
    }
}
#endif