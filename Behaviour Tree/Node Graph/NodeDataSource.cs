public class NodeDataSource {
    private Dictionary<NodeType, NodeBuilderDetails> nodeFactory = new Dictionary<NodeType, NodeBuilderDetails>();

    public string[] NodeOptions => nodeFactory.Values().Select(x => x.Name).ToArray();
    public NodeType[] options => nodeFactory.Keys().ToArray();



    void RegisterNodeBuilder(NodeType nodeType, string Description, string Name, INodeBuilder nodeBuilder) {
        nodeFactory[nodeType] = new NodeBuilderDetails {
            NodeBuilder = nodeBuilder,
            Name = Name,
            Desscription = Description
        };
    }
    GraphElement Build(NodeType nodeType) {

    }
}

public class NodeBuilderDetails {
    INodeBuilder NodeBuilder;
    string Name;
    string Description;
}