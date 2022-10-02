﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System;


public class BehaviourGraph : EditorWindow
{
    private BehaviourGraphView GraphView;
    private NodeDataSource nodeDataSource;
    private string FileName= "New Behaviour Tree";
    string[] NodeOptions => this.nodeDataSource.NodeOptions;
    int index = 0;
    public NodeType[] options => this.nodeDataSource.options;
   [ MenuItem("Graph/Behaviour Tree Graph")]
    public static void OpenBehaviourGraphWindow()
    {
        var window = GetWindow<BehaviourGraph>();
        window.titleContent = new GUIContent(text: "Behaviour Tree Editor");
    }
    private void ConstructGraphView()
    {
        this.nodeDataSource = new NodeDataSource();
        this.nodeDataSource.RegisterNodeBuilder();
        this.GraphView = new BehaviourGraphView
        {
            name = "Behaviuor Graph"

        };
        this.GraphView.StretchToParentSize();
        rootVisualElement.Add(this.GraphView);
    }
    Rect rect;
    private void GenerateToolbar()
    {
        var toolbar = new Toolbar();

        var fileNameTextField = new TextField(label: "File Name:");
        fileNameTextField.SetValueWithoutNotify(this.FileName);
        fileNameTextField.MarkDirtyRepaint();
        fileNameTextField.RegisterValueChangedCallback((EventCallback<ChangeEvent<string>>)(evt => this.FileName = evt.newValue));
        toolbar.Add(fileNameTextField);
        toolbar.Add(child: new Button( clickEvent:() => RequestDataOperation(true)){ text = "Save Data"});
        toolbar.Add(child: new Button(clickEvent: () => RequestDataOperation(false)) { text = "Load Data" });

 
            var nodeTypes = new List<NodeType> { NodeType.PARALLEL,NodeType.SELECTOR,NodeType.SEQUENCE };
            var popup = new PopupField<NodeType>("Node Types",nodeTypes,0);
            var CreateButton = new Button(clickEvent: () =>
            {
                this.GraphView.CreateNode("Behaviour Node",nodeTypes[popup.index]);
            });
            CreateButton.text = "Create Node";
            popup.Add(CreateButton);
        toolbar.Add(popup);

            //this.GraphView.CreateNode("Behaviour Node");


        rootVisualElement.Add(toolbar);
    }

    private void RequestDataOperation(bool save)
    {
        if (string.IsNullOrEmpty(this.FileName))
        {
            EditorUtility.DisplayDialog(title: "Invalid File Name", message: "Please enter a valid file name.", ok: "OK");
        }
        var saveUtility = GraphSaveUtility.GetInstance(GraphView);
        if(save)
        {
            saveUtility.SaveGraph(this.FileName);
        }
        else
        {
            saveUtility.LoadGraph(this.FileName);
        }
    }

    private void OnEnable()
    {
        this.ConstructGraphView();
        this.GenerateToolbar();
    }
    private void OnDisable()
    {
        rootVisualElement.Remove(this.GraphView);
    }
}
