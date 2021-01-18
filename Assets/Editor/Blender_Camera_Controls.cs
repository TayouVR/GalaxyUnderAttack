using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;

public class CamControl : EditorWindow {
	/*
	 * This an extension of Marc Kusters BlenderCameraControls script found here:
	 * 
	 * http://wiki.unity3d.com/index.php/Blender_Camera_Controls
	 */

	private static List<Axis> currentlyEditingAxis = new List<Axis>();
	private static Tool tool;
	private static GameObject currentSelection;
	private static MyTransform originalTransform;
	private static Vector2 mousePosition;
	private static string movement;
	
	
	private static bool isEnabled 	= true;
 
	[MenuItem("Window/" + "CamControl Window")]
	public static void Init() {
		CamControl window = GetWindow<CamControl>();
		window.title = "CamControl";
		window.minSize = new Vector2(10,10);
	}	
 
	public void OnEnable() {
		SceneView.duringSceneGui += OnScene;
	}
 
	public void OnGUI() {
 
		GUILayoutOption[] options = {GUILayout.MinWidth(5) };
 
		if(GUILayout.Button("Close", options))
			GetWindow<CamControl>().Close();
 
		//Enable or disable button
		if (isEnabled) {
			if(GUILayout.Button("Enabled", options))
				isEnabled = false;
		} else {
			if (GUILayout.Button("Disabled", options))
				isEnabled = true;
		}
	}
 
	private static void OnScene(SceneView sceneview) {
 
		if (!isEnabled) return;
 
		UnityEditor.SceneView sceneView;
		Vector3 eulerAngles;
     	Event current;
     	Quaternion rotHelper;
 
        current = Event.current;
 
        sceneView = UnityEditor.SceneView.lastActiveSceneView;
 
        eulerAngles = sceneView.camera.transform.rotation.eulerAngles;
        rotHelper = sceneView.camera.transform.rotation;
        
        switch (tool) {
	        case Tool.Rotation:
		        if (currentlyEditingAxis.Contains(Axis.X)) {
			        
		        }
		        if (currentlyEditingAxis.Contains(Axis.Y)) {
			        
		        }
		        if (currentlyEditingAxis.Contains(Axis.Z)) {
			        
		        }
		        break;
	        case Tool.Position:
		        var position1 = currentSelection.transform.position;
		        if (currentlyEditingAxis.Contains(Axis.X)) {
			        if (!string.IsNullOrEmpty(movement)) {
				        currentSelection.transform.position = new Vector3(position1.x + float.Parse(movement), position1.y, position1.z);
			        } else {
				        currentSelection.transform.position = new Vector3(position1.x + (mousePosition.x - current.mousePosition.x) * 0.1f, position1.y, position1.z);
			        }
		        }
		        if (currentlyEditingAxis.Contains(Axis.Y)) {
			        if (!string.IsNullOrEmpty(movement)) {
				        currentSelection.transform.position = new Vector3(position1.x, position1.y + float.Parse(movement), position1.z);
			        } else {
				        currentSelection.transform.position = new Vector3(position1.x, position1.y + (mousePosition.x - current.mousePosition.x) * 0.1f, position1.z);
			        }
		        }
		        if (currentlyEditingAxis.Contains(Axis.Z)) {
			        if (!string.IsNullOrEmpty(movement)) {
				        currentSelection.transform.position = new Vector3(position1.x, position1.y, position1.z + float.Parse(movement));
			        } else {
				        currentSelection.transform.position = new Vector3(position1.x, position1.y, position1.z + (mousePosition.x - current.mousePosition.x) * 0.1f);
			        }
		        }
		        if (currentlyEditingAxis.Count <= 0) {
			        currentSelection.transform.position = new Vector3(position1.x, position1.y, position1.z + (mousePosition.x - current.mousePosition.x) * 0.1f);
		        }
		        break;
	        case Tool.Scaling:
		        var scale1 = currentSelection.transform.localScale;
		        var scale2 = originalTransform.localScale;
		        if (currentlyEditingAxis.Contains(Axis.X)) {
			        if (!string.IsNullOrEmpty(movement)) {
				        currentSelection.transform.localScale = new Vector3(scale2.x * float.Parse(movement), scale2.y, scale2.z);
			        } else {
				        currentSelection.transform.localScale = new Vector3(scale1.x + (mousePosition.x - current.mousePosition.x) * 0.1f, scale1.y, scale1.z);
			        }
		        }
		        if (currentlyEditingAxis.Contains(Axis.Y)) {
			        if (!string.IsNullOrEmpty(movement)) {
				        currentSelection.transform.localScale = new Vector3(scale2.x, scale2.y * float.Parse(movement), scale2.z);
			        } else {
				        currentSelection.transform.localScale = new Vector3(scale1.x, scale1.y + (mousePosition.x - current.mousePosition.x) * 0.1f, scale1.z);
			        }
		        }
		        if (currentlyEditingAxis.Contains(Axis.Z)) {
			        if (!string.IsNullOrEmpty(movement)) {
				        currentSelection.transform.localScale = new Vector3(scale2.x, scale2.y, scale2.z * float.Parse(movement));
			        } else {
				        currentSelection.transform.localScale = new Vector3(scale1.x, scale1.y, scale1.z + (mousePosition.x - current.mousePosition.x) * 0.1f);
			        }
		        }
		        if (currentlyEditingAxis.Count <= 0) {
			        currentSelection.transform.localScale = new Vector3(scale1.x, scale1.y, scale1.z + (mousePosition.x - current.mousePosition.x) * 0.1f);
		        }
		        break;
	        case Tool.NULL:
		        break;
        }

        mousePosition = current.mousePosition;
        
        /*
        Event e = Event.current;
        if (e.button == 0 && e.isMouse) {
	        originalTransform = new MyTransform(currentSelection.transform);
	        currentlyEditingAxis.Clear();
	        tool = Tool.NULL;
        } else if (e.button == 1) {
	        if (currentSelection != null) {
		        SetTransform(originalTransform);
	        }
	        currentlyEditingAxis.Clear();
	        tool = Tool.NULL;
        } else if (e.button == 2) {
	        Debug.Log("Middle Click");
        } else if (e.button > 2) {
	        Debug.Log("Another button in the mouse clicked");
        }*/
        
        if (!current.isKey || current.type != EventType.KeyDown)
	        return;
	    
        switch (current.keyCode) {
	        case KeyCode.Alpha0:
		        movement = movement + "0";
		        break;
	        case KeyCode.Alpha1:
		        movement = movement + "1";
		        break;
	        case KeyCode.Alpha2:
		        movement = movement + "2";
		        break;
	        case KeyCode.Alpha3:
		        movement = movement + "3";
		        break;
	        case KeyCode.Alpha4:
		        movement = movement + "4";
		        break;
	        case KeyCode.Alpha5:
		        movement = movement + "5";
		        break;
	        case KeyCode.Alpha6:
		        movement = movement + "6";
		        break;
	        case KeyCode.Alpha7:
		        movement = movement + "7";
		        break;
	        case KeyCode.Alpha8:
		        movement = movement + "8";
		        break;
	        case KeyCode.Alpha9:
		        movement = movement + "9";
		        break;
	        case KeyCode.Period:
		        movement = movement + ".";
		        break;
	        case KeyCode.Backspace:
		        movement = movement.Substring(0, movement.Length-1);
		        break;
	        /*case KeyCode.Delete:
		        break;
	        case KeyCode.LeftArrow:
		        break;
	        case KeyCode.RightArrow:
		        break;*/
            case KeyCode.Keypad1:
                if (current.control == false)
                    sceneView.LookAtDirect(SceneView.lastActiveSceneView.pivot, Quaternion.Euler(new Vector3(0f, 360f, 0f)));
                else
                    sceneView.LookAtDirect(SceneView.lastActiveSceneView.pivot, Quaternion.Euler(new Vector3(0f, 180f, 0f)));
                break;
            case KeyCode.Keypad2:
                sceneView.LookAtDirect(SceneView.lastActiveSceneView.pivot, rotHelper * Quaternion.Euler(new Vector3(-15f, 0f, 0f)));
                break;
            case KeyCode.Keypad3:
                if (current.control == false)
                    sceneView.LookAtDirect(SceneView.lastActiveSceneView.pivot, Quaternion.Euler(new Vector3(0f, 270f, 0f)));
                else
                    sceneView.LookAtDirect(SceneView.lastActiveSceneView.pivot, Quaternion.Euler(new Vector3(0f, 90f, 0f)));
                break;
            case KeyCode.Keypad4:
                sceneView.LookAtDirect(SceneView.lastActiveSceneView.pivot, Quaternion.Euler(new Vector3(eulerAngles.x, eulerAngles.y + 15f, eulerAngles.z)));
                break;
            case KeyCode.Keypad5:
                sceneView.orthographic = !sceneView.orthographic;
                break;
            case KeyCode.Keypad6:
                sceneView.LookAtDirect(SceneView.lastActiveSceneView.pivot, Quaternion.Euler(new Vector3(eulerAngles.x, eulerAngles.y - 15f, eulerAngles.z)));
                break;
            case KeyCode.Keypad7:
                if (current.control == false)
                    sceneView.LookAtDirect(SceneView.lastActiveSceneView.pivot, Quaternion.Euler(new Vector3(90f, 0f, 0f)));
                else
                    sceneView.LookAtDirect(SceneView.lastActiveSceneView.pivot, Quaternion.Euler(new Vector3(270f, 0f, 0f)));
                break;
            case KeyCode.Keypad8:
                sceneView.LookAtDirect(SceneView.lastActiveSceneView.pivot, rotHelper * Quaternion.Euler(new Vector3(15f, 0f, 0f)));
                break;
            case KeyCode.Keypad9:
	            sceneView.LookAtDirect(SceneView.lastActiveSceneView.pivot, Quaternion.Euler(new Vector3(eulerAngles.x, eulerAngles.y + 180f, eulerAngles.z)));
	            break;
            case KeyCode.KeypadPeriod:
                if (Selection.transforms.Length == 1)
                    sceneView.LookAtDirect(Selection.activeTransform.position, sceneView.camera.transform.rotation);
                else if (Selection.transforms.Length > 1) {
                    Vector3 tempVec = new Vector3();
                    for (int i = 0; i < Selection.transforms.Length; i++) {
                        tempVec += Selection.transforms[i].position;
                    }
                    sceneView.LookAtDirect((tempVec / Selection.transforms.Length), sceneView.camera.transform.rotation);
                }
                break;
            case KeyCode.KeypadMinus:
                SceneView.RepaintAll();
                sceneView.size *= 1.1f;
                break;
            case KeyCode.KeypadPlus:
                SceneView.RepaintAll();
                sceneView.size /= 1.1f;
                break;
            case KeyCode.G:
                if (currentSelection != null) {
					SetTransform(originalTransform);
				}
	            tool = Tool.Position;
	            currentSelection = Selection.activeGameObject;
	            originalTransform = new MyTransform(currentSelection.transform);
	            break;
            case KeyCode.R:
                if (currentSelection != null) {
                    SetTransform(originalTransform);
                }
				tool = Tool.Rotation;
	            currentSelection = Selection.activeGameObject;
	            originalTransform = new MyTransform(currentSelection.transform);
	            break;
            case KeyCode.S:
                if (currentSelection != null) {
                    SetTransform(originalTransform);
                }
				tool = Tool.Scaling;
	            currentSelection = Selection.activeGameObject;
	            originalTransform = new MyTransform(currentSelection.transform);
	            break;
            case KeyCode.X:
	            currentlyEditingAxis.Clear();
	            if (!current.shift) {
		            currentlyEditingAxis.Add(Axis.X);
	            } else {
		            currentlyEditingAxis.Add(Axis.Y);
		            currentlyEditingAxis.Add(Axis.Z);
	            }
	            break;
            case KeyCode.Y:
	            currentlyEditingAxis.Clear();
	            if (!current.shift) {
		            currentlyEditingAxis.Add(Axis.Y);
	            } else {
		            currentlyEditingAxis.Add(Axis.X);
		            currentlyEditingAxis.Add(Axis.Z);
	            }
	            break;
            case KeyCode.Z:
	            currentlyEditingAxis.Clear();
	            if (!current.shift) {
		            currentlyEditingAxis.Add(Axis.Z);
	            } else {
		            currentlyEditingAxis.Add(Axis.Y);
		            currentlyEditingAxis.Add(Axis.X);
	            }
	            break;
            case KeyCode.Return:
	            originalTransform = new MyTransform(currentSelection.transform);
	            currentlyEditingAxis.Clear();
	            tool = Tool.NULL;
	            movement = "";
	            break;
        }
 
	}

	private static void SetTransform(MyTransform t) {
		currentSelection.transform.position = t.position;
		currentSelection.transform.rotation = t.rotation;
		currentSelection.transform.localScale = t.localScale;
	}
 
	public void OnDestroy() {
		SceneView.onSceneGUIDelegate -= OnScene;
	}

	private enum Axis {
		X,
		Y,
		Z
	}
	
	private enum Tool {
		Rotation,
		Position,
		Scaling,
		NULL
	}
	
	private struct MyTransform {
		public Vector3 position;
		public Quaternion rotation;
		public Vector3 localScale;

		public MyTransform(Transform t) {
			position = t.position;
			rotation = t.rotation;
			localScale = t.localScale;
		}
	}
}