using System;
using System.Xml;
using System.IO;

namespace XMLReadWrite
{
    /// <summary>
    /// 
    /// </summary>
    public class XMLRobotInfoFile
    {
        #region Private Const Members
        //RobotInfo tags
        private const string C_STR_TAG_ROOT = "root";
        private const string C_STR_TAG_ROBOT_INFO = "RobotInfo";
        private const string C_STR_TAG_INSTANCE = "Instance";
        private const string C_STR_TAG_VENDOR = "Vendor";
        private const string C_STR_TAG_CONTROLLER = "Controller";
        private const string C_STR_TAG_RCS = "RCS";
        private const string C_STR_TAG_MANIPULATORTYPE = "ManipulatorType";

        private const string C_STR_ATTR_DATE = "Date";
        private const string C_STR_ATTR_NAME = "Name";
        private const string C_STR_ATTR_VERSION = "Version";
        //RobotInfo attributes
        #endregion

        #region Private Members
        string m_path;
        string m_date = null;
        string m_vendor = null;
        //string m_controllerName;
        //string m_controllerVersion;
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        public XMLRobotInfoFile(string path)
        {
            m_path = path;
        }
        #endregion

        #region Public Properties

        /// <summary>
        /// 
        /// </summary>
        private string Date
        {
            get { return m_date; }
        }

        private string Vendor
        {
            get { return m_vendor; }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        private bool ReadRobotInfoBloc()
        {

            XmlTextReader xmlReader = new XmlTextReader(m_path);
            try
            {
                xmlReader.WhitespaceHandling = WhitespaceHandling.None;
                // read elements with attributes
                while (xmlReader.Read())
                {
                    switch (xmlReader.NodeType)
                    {
                        case XmlNodeType.Element:
                            //<root>
                            if (xmlReader.Name == C_STR_TAG_ROOT)
                                continue;

                            //<RobotInfo>
                            if (xmlReader.Name == C_STR_TAG_ROBOT_INFO)
                            {
                                if (xmlReader.MoveToNextAttribute())
                                {
                                    string date = xmlReader.Value;
                                    Console.WriteLine("Date:{0}", date);
                                }
                                continue;
                            }

                            if (xmlReader.Name == C_STR_TAG_VENDOR)
                            {
                                if (xmlReader.MoveToNextAttribute())
                                {
                                    string vendor = xmlReader.Value;
                                    Console.WriteLine("Vendor:{0}", vendor);
                                }
                                continue;
                            }

                            if (xmlReader.Name == C_STR_TAG_CONTROLLER)
                            {
                                if (xmlReader.MoveToNextAttribute())
                                {
                                    string name = xmlReader.Value;
                                    Console.WriteLine("Controller name:{0}", name);
                                }

                                if (xmlReader.MoveToNextAttribute())
                                {
                                    string version = xmlReader.Value;
                                    Console.WriteLine("Controller version:{0}", version);
                                }
                                continue;
                            }

                            if (xmlReader.Name == C_STR_TAG_RCS)
                            {
                                if (xmlReader.MoveToNextAttribute())
                                {
                                    string rcsVersion = xmlReader.Value;
                                    Console.WriteLine("Controller name:{0}", rcsVersion);
                                }
                                continue;
                            }

                            if (xmlReader.Name == C_STR_TAG_MANIPULATORTYPE)
                            {
                                if (xmlReader.MoveToNextAttribute())
                                {
                                    string type = xmlReader.Value;
                                    Console.WriteLine("Manipulator type :{0}", type);
                                }
                                continue;
                            }
                            break;
                        case XmlNodeType.EndElement:
                            Console.WriteLine("********END ELEMENT*********");
                            xmlReader.Close();
                            return true;
                    }
                }

            }
            catch
            {
                xmlReader.Close();
                return false;
            }

            return true;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool UpdateRobotInfoFile()
        {
            if (File.Exists(m_path))
            {
                if (ReadRobotInfoBloc())
                {

                }
                else
                    return false;

                return true;
            }
            //else
            //    (WriteRobot)
            return true;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool WriteRobotInfoBlock()
        {
            string currDateStamp = DateTime.Today.ToShortDateString();
            currDateStamp = currDateStamp.Replace('.', '-');

            XmlDocument robotInfoFile = new XmlDocument();
            robotInfoFile.Load(m_path);
            XmlNode root = robotInfoFile.DocumentElement;

            //<RobotInfo>
            XmlElement currRobotInfoElem = robotInfoFile.CreateElement(C_STR_TAG_ROBOT_INFO);
            currRobotInfoElem.SetAttribute(C_STR_ATTR_DATE, currDateStamp);

            //<Instance>
            XmlElement currInstanceElem = robotInfoFile.CreateElement(C_STR_TAG_INSTANCE);
            currInstanceElem.SetAttribute(C_STR_ATTR_NAME, "test");
            currRobotInfoElem.AppendChild(currInstanceElem);

            //<Vendor>
            XmlElement currVendorElem = robotInfoFile.CreateElement(C_STR_TAG_VENDOR);
            currVendorElem.SetAttribute(C_STR_ATTR_NAME, "ABB");
            currRobotInfoElem.AppendChild(currVendorElem);

            //<Controller>
            XmlElement currControllerElem = robotInfoFile.CreateElement(C_STR_TAG_CONTROLLER);
            currControllerElem.SetAttribute(C_STR_ATTR_NAME, "ABB-Rapid-Volvo");
            currControllerElem.SetAttribute(C_STR_ATTR_VERSION, "4.0 S4C");
            currRobotInfoElem.AppendChild(currControllerElem);

            //<RCS>
            XmlElement currRCSElem = robotInfoFile.CreateElement(C_STR_TAG_RCS);
            currRCSElem.SetAttribute(C_STR_ATTR_VERSION, "ABB-Rapid-Volvo");
            currRobotInfoElem.AppendChild(currRCSElem);

            //<ManipulatorType>
            XmlElement currManipulatorTypeElem = robotInfoFile.CreateElement(C_STR_TAG_MANIPULATORTYPE);
            currManipulatorTypeElem.SetAttribute(C_STR_ATTR_NAME, "ABB-Rapid-Volvo");
            currRobotInfoElem.AppendChild(currManipulatorTypeElem);

            root.PrependChild(currRobotInfoElem);
            robotInfoFile.Save(m_path);

            return true;
        }

        public void TestCreateNode()
        {


            XmlDocument doc = new XmlDocument();
            doc.LoadXml("<book>" +
                        "  <title>Oberon's Legacy</title>" +
                        "  <price>5.95</price>" +
                        "</book>");

            // Create a new element node.
            XmlNode newElem = doc.CreateNode("element", "pages", "");
            newElem.InnerText = "290";

            Console.WriteLine("Add the new element to the document...");
            XmlElement root = doc.DocumentElement;
            root.AppendChild(newElem);

            Console.WriteLine("Display the modified XML document...");
            Console.WriteLine(doc.OuterXml);
        }
        #endregion
    }

    /// <summary>
    /// 
    /// </summary>
    class Test
    {
        #region Private Members
        XmlDocument m_dataFile;
        XmlElement m_root;
        string m_ns = @"http://sample/namespace";
        #endregion

        #region Constructors
        public Test()
        {
            m_dataFile = new XmlDocument();

            m_root = m_dataFile.CreateElement("datas");
            XmlAttribute nsAttribute = m_dataFile.CreateAttribute("xmlns", "xsi", "http://www.w3.org/2000/xmlns/");
            nsAttribute.Value = m_ns;
            m_root.Attributes.Append(nsAttribute);
            m_dataFile.AppendChild(m_root);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// 
        /// </summary>
        public static void testParent()
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("root");

            string ns = "http://sample/namespace";
            XmlAttribute nsAttribute = doc.CreateAttribute("xmlns", "xx", "http://www.w3.org/2000/xmlns/");
            nsAttribute.Value = ns;
            root.Attributes.Append(nsAttribute);

            doc.AppendChild(root);
            XmlElement child = doc.CreateElement("child");
            root.AppendChild(child);
            XmlAttribute newAttribute = doc.CreateAttribute("xx", "abc", ns);
            newAttribute.Value = "ddd";
            child.Attributes.Append(newAttribute);

            doc.Save(Console.Out);
        }


        /// <summary>
        /// 
        /// </summary>
        public void testParetAtt()
        {

            // toolNode
            XmlElement toolDataNode = m_dataFile.CreateElement("data");
            m_root.AppendChild(toolDataNode);

            toolDataNode.SetAttribute("test1", "private");
            toolDataNode.SetAttribute("test2", "2");
            toolDataNode.SetAttribute("test3", "3");
            toolDataNode.SetAttribute("test4", "3");


            XmlAttribute atr = m_dataFile.CreateAttribute("xsi", "type", m_ns);
            atr.Value = "array";
            toolDataNode.Attributes.Append(atr);

            m_dataFile.Save(Console.Out);


            Console.Out.WriteLine("Root inner text:{0}", m_root.InnerXml);
            Console.Out.WriteLine("---------------------------------");
            Console.Out.WriteLine(toolDataNode.OuterXml);
        }
        #endregion
    }

    /// <summary>
    /// 
    /// </summary>
    public class PositionDataXmlfileWritter
    {
        #region Private Methods
        /// <summary>
        /// Indent XML code
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        private string FormatXML(XmlDocument doc)
        {
            // Create a stream buffer that can be read as a string
            using (StringWriter sw = new StringWriter())

            // Create a specialized writer for XML code
            using (XmlTextWriter xtw = new XmlTextWriter(sw))
            {
                // Set the writer to use indented (hierarchical) elements
                xtw.Formatting = System.Xml.Formatting.Indented;

                // Write the XML document to the stream
                doc.WriteTo(xtw);

                // Return the stream as a string
                return sw.ToString();
            }
        }

        /// <summary>
        /// Create frame section node in XML file
        /// </summary>
        /// <param name="dataFile"></param>
        /// <returns></returns>
        private XmlNode GetFrameSectionNode(XmlDocument dataFile)
        {
            //FrameSection
            XmlNode frameSectionNode = dataFile.CreateElement("frameSection");

            // frameNode
            XmlElement frameNode = dataFile.CreateElement("frame");
            frameNode.SetAttribute("Name", "base1");
            frameNode.SetAttribute("Public", "base1");
            frameNode.SetAttribute("privilege", "true");
            frameSectionNode.AppendChild(frameNode);

            //fFather
            XmlElement fFather = dataFile.CreateElement("fFather");
            fFather.SetAttribute("name", "world");
            fFather.SetAttribute("alias", "");
            fFather.SetAttribute("fatherIndex", "0");
            frameNode.AppendChild(fFather);

            //ValueFrame
            XmlElement valueFrame = dataFile.CreateElement("valueFrame");
            valueFrame.SetAttribute("index", "0");

            //Frame coordinates
            XmlElement tfValue = dataFile.CreateElement("tfValue");

            //translation
            tfValue.SetAttribute("x", "1");
            tfValue.SetAttribute("y", "2");
            tfValue.SetAttribute("z", "3");

            //rotation
            tfValue.SetAttribute("rx", "1");
            tfValue.SetAttribute("ry", "2");
            tfValue.SetAttribute("rz", "3");
            valueFrame.AppendChild(tfValue);

            frameNode.AppendChild(valueFrame);
            return frameSectionNode;
        }

        /// <summary>
        /// Create joint section node in XML file
        /// </summary>
        /// <param name="dataFile"></param>
        /// <returns></returns>
        private XmlNode GetJointNode(XmlDocument dataFile)
        {
            XmlNode jointSection = dataFile.CreateElement("jointSection");

            // frameNode
            XmlElement jointNode = dataFile.CreateElement("joint");
            jointNode.SetAttribute("Name", "base1");
            jointNode.SetAttribute("Public", "base1");
            jointNode.SetAttribute("privilege", "true");
            jointSection.AppendChild(jointNode);

            //ValueFrame
            XmlElement valueJoint = dataFile.CreateElement("valueJoint");
            valueJoint.SetAttribute("index", "0");
            jointNode.AppendChild(valueJoint);

            //ValueFrame
            XmlElement jointValue = dataFile.CreateElement("jointValue");
            jointValue.SetAttribute("j1", "1");
            jointValue.SetAttribute("j2", "2");
            jointValue.SetAttribute("j3", "3");
            jointValue.SetAttribute("j4", "1");
            jointValue.SetAttribute("j5", "2");
            jointValue.SetAttribute("j6", "3");

            valueJoint.AppendChild(jointValue);

            return jointSection;
        }

        /// <summary>
        /// Create tool section node in XML file
        /// </summary>
        /// <param name="dataFile"></param>
        /// <returns></returns>
        private XmlNode GetToolsSectionNode(XmlDocument dataFile)
        {
            XmlNode toolsSectionNode = dataFile.CreateElement("toolSection");

            // toolNode
            XmlElement toolNode = dataFile.CreateElement("tool");
            toolNode.SetAttribute("Name", "base1");
            toolNode.SetAttribute("Public", "base1");
            toolNode.SetAttribute("privilege", "true");
            toolsSectionNode.AppendChild(toolNode);




            //pFather
            XmlElement tFather = dataFile.CreateElement("tFather");
            tFather.SetAttribute("name", "world");
            tFather.SetAttribute("alias", "");
            tFather.SetAttribute("fatherIndex", "0");
            toolNode.AppendChild(tFather);

            //ValuePoint
            XmlElement valueTool = dataFile.CreateElement("valueTool");
            valueTool.SetAttribute("index", "0");
            toolNode.AppendChild(valueTool);

            //Tool coordinates
            XmlElement ttValue = dataFile.CreateElement("tpValue");

            //translation
            ttValue.SetAttribute("x", "1");
            ttValue.SetAttribute("y", "2");
            ttValue.SetAttribute("z", "3");

            //rotation
            ttValue.SetAttribute("rx", "1");
            ttValue.SetAttribute("ry", "2");
            ttValue.SetAttribute("rz", "3");
            valueTool.AppendChild(ttValue);

            //Tool coordinates
            XmlElement io = dataFile.CreateElement("io");
            io.SetAttribute("name", "valve1");
            io.SetAttribute("alias", "io");
            io.SetAttribute("ioIndex", "0");
            io.SetAttribute("open", "0");
            io.SetAttribute("close", "0");
            valueTool.AppendChild(io);

            return toolsSectionNode;
        }

        /// </summary>
        /// <param name="dataFile"></param>
        /// <returns></returns>
        private XmlNode GetMdescNode(XmlDocument dataFile)
        {
            XmlNode mdescNode = dataFile.CreateElement("mdescSection");
            return mdescNode;
        }

        /// <summary>
        /// Create tool section node in XML file
        /// </summary>
        /// <param name="dataFile"></param>
        /// <returns></returns>
        private XmlNode GetPointsSectionNode(XmlDocument dataFile)
        {
            XmlNode pointsSetionNode = dataFile.CreateElement("pointSection");

            // frameNode
            XmlElement pointNode = dataFile.CreateElement("point");
            pointNode.SetAttribute("Name", "base1");
            pointNode.SetAttribute("Public", "base1");
            pointNode.SetAttribute("privilege", "true");

            //pFather
            XmlElement pFather = dataFile.CreateElement("fFather");
            pFather.SetAttribute("name", "world");
            pFather.SetAttribute("alias", "");
            pFather.SetAttribute("fatherIndex", "0");
            pointNode.AppendChild(pFather);

            //ValuePoint
            XmlElement valuePoint = dataFile.CreateElement("valuePoint");
            valuePoint.SetAttribute("index", "0");
            pointNode.AppendChild(valuePoint);

            //Frame coordinates
            XmlElement tpValue = dataFile.CreateElement("tpValue");

            //translation
            tpValue.SetAttribute("x", "1");
            tpValue.SetAttribute("y", "2");
            tpValue.SetAttribute("z", "3");

            //rotation
            tpValue.SetAttribute("rx", "1");
            tpValue.SetAttribute("ry", "2");
            tpValue.SetAttribute("rz", "3");
            valuePoint.AppendChild(tpValue);

            pointsSetionNode.AppendChild(pointNode);
            return pointsSetionNode;
        }

        /// <summary>
        /// Create tool section node in XML file
        /// </summary>
        /// <param name="dataFile"></param>
        /// <returns></returns>
        private XmlElement GetStartProgramNode(XmlDocument startFile)
        {
            XmlElement programNode = startFile.CreateElement("program");
            programNode.SetAttribute("name", "start");
            programNode.SetAttribute("public", "true");

            // sourceNode
            XmlElement sourceNode = startFile.CreateElement("source");
            programNode.AppendChild(sourceNode);


            // sourceNode
            XmlElement codeNode = startFile.CreateElement("code");
            codeNode.InnerText = "\r\nbegin\r\nend\r\n";
            sourceNode.AppendChild(codeNode);
            return programNode;
        }

        /// <summary>
        /// Create tool section node in XML file
        /// </summary>
        /// <param name="dataFile"></param>
        /// <returns></returns>
        private XmlElement GetStopProgramNode(XmlDocument startFile)
        {
            XmlElement programNode = startFile.CreateElement("program");
            programNode.SetAttribute("name", "stop");
            programNode.SetAttribute("public", "true");

            // sourceNode
            XmlElement sourceNode = startFile.CreateElement("source");
            programNode.AppendChild(sourceNode);


            // sourceNode
            XmlElement codeNode = startFile.CreateElement("code");
            codeNode.InnerText = "\r\nbegin\r\nend\r\n";
            sourceNode.AppendChild(codeNode);
            return programNode;
        }

        /// <summary>
        /// Create tool section node in XML file
        /// </summary>
        /// <param name="dataFile"></param>
        /// <returns></returns>
        private XmlElement GetMainProgramNode(XmlDocument programFile)
        {
            XmlElement programNode = programFile.CreateElement("program");
            programNode.SetAttribute("name", "pr3");
            programNode.SetAttribute("public", "true");

            // sourceNode
            XmlElement sourceNode = programFile.CreateElement("source");
            programNode.AppendChild(sourceNode);

            // sourceNode
            XmlElement codeNode = programFile.CreateElement("code");
            codeNode.InnerText = "\r\nbegin\r\nend\r\n";
            sourceNode.AppendChild(codeNode);

            sourceNode.InnerXml = "test";
            return programNode;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Writes data file
        /// </summary>
        public void WriteDataFile()
        {
            //dtx Data File
            XmlDocument dataFile = new XmlDocument();
            dataFile.PreserveWhitespace = true;

            XmlNode docNode = dataFile.CreateXmlDeclaration("1.0", "ISO-8859-1", null);
            dataFile.AppendChild(docNode);

            //dataList node
            XmlNode dataListNode = dataFile.CreateElement("dataList");
            dataFile.AppendChild(dataListNode);

            //aioSectionNode
            XmlNode aioSectionNode = dataFile.CreateElement("aioSection");
            dataListNode.AppendChild(aioSectionNode);

            //boolSectionNode
            XmlNode boolSectionNode = dataFile.CreateElement("boolSection");
            dataListNode.AppendChild(boolSectionNode);

            //boolSectionNode
            XmlNode dioSectionNode = dataFile.CreateElement("dioSection");
            dataListNode.AppendChild(dioSectionNode);

            //frameSectionNode
            XmlNode frameSectionNode = GetFrameSectionNode(dataFile);
            dataListNode.AppendChild(frameSectionNode);

            //test
            XmlDocument temp = new XmlDocument();
            XmlNode tempNode = GetFrameSectionNode(temp);
            temp.AppendChild(tempNode);


            //jointSectionNode
            XmlNode jointSectionNode = GetJointNode(dataFile);
            dataListNode.AppendChild(jointSectionNode);

            //mdescSectionNode
            XmlNode mdescSectionNode = dataFile.CreateElement("mdescSection");
            dataListNode.AppendChild(mdescSectionNode);

            //pointSectionNode
            XmlNode pointSectionNode = GetPointsSectionNode(dataFile);
            dataListNode.AppendChild(pointSectionNode);

            //sioSectionNode
            XmlNode sioSectionnNode = dataFile.CreateElement("sioSection");
            dataListNode.AppendChild(sioSectionnNode);

            //stringSectionNode
            XmlNode stringSectionNode = dataFile.CreateElement("stringSection");
            dataListNode.AppendChild(stringSectionNode);

            //toolSectionNode
            XmlNode toolSectionNode = GetToolsSectionNode(dataFile);
            dataListNode.AppendChild(toolSectionNode);

            //trsfSectionNode
            XmlNode trsfSectionNode = dataFile.CreateElement("trsfSection");
            dataListNode.AppendChild(trsfSectionNode);

            //***************************************************************           
            Console.WriteLine(FormatXML(dataFile));
            Console.WriteLine("--------------------END--------------------");
            Console.Write(FormatXML((temp)));
            Console.WriteLine("--------------------END--------------------");
            Console.ReadKey();
        }

        /// <summary>
        /// Writes program file
        /// </summary>
        public void WriteProgramFile()
        {
            XmlDocument programFile = new XmlDocument();
            programFile.PreserveWhitespace = true;

            XmlNode docNode = programFile.CreateXmlDeclaration("1.0", "ISO-8859-1", null);
            programFile.AppendChild(docNode);

            //programList node
            XmlElement programListNode = programFile.CreateElement("programList");
            programListNode.SetAttribute("xmlns", "ProgramNameSpace");
            programFile.AppendChild(programListNode);

            //aioSectionNode
            XmlElement programNode = GetMainProgramNode(programFile);
            programListNode.AppendChild(programNode);

            Console.WriteLine(FormatXML(programFile));
            Console.WriteLine("--------------------END--------------------");
            Console.ReadKey();
        }

        /// <summary>
        /// Writes start file
        /// </summary>
        public void WirteStartPgxFile()
        {
            XmlDocument startPgxFile = new XmlDocument();
            startPgxFile.PreserveWhitespace = true;

            XmlNode docNode = startPgxFile.CreateXmlDeclaration("1.0", "ISO-8859-1", null);
            startPgxFile.AppendChild(docNode);

            //programList node
            XmlElement programListNode = startPgxFile.CreateElement("programList");
            programListNode.SetAttribute("xmlns", "ProgramNameSpace");
            startPgxFile.AppendChild(programListNode);

            //aioSectionNode
            XmlElement programNode = GetStartProgramNode(startPgxFile);
            programListNode.AppendChild(programNode);

            Console.WriteLine(FormatXML(startPgxFile));
            Console.WriteLine("--------------------END--------------------");
            Console.ReadKey();
        }

        /// <summary>
        /// Writes stop file
        /// </summary>
        public void WirteStopPgxFile()
        {
            XmlDocument stopPgxFile = new XmlDocument();
            stopPgxFile.PreserveWhitespace = true;

            XmlNode docNode = stopPgxFile.CreateXmlDeclaration("1.0", "ISO-8859-1", null);
            stopPgxFile.AppendChild(docNode);

            //programList node
            XmlElement programListNode = stopPgxFile.CreateElement("programList");
            programListNode.SetAttribute("xmlns", "ProgramNameSpace");
            stopPgxFile.AppendChild(programListNode);

            //aioSectionNode
            XmlElement programNode = GetStopProgramNode(stopPgxFile);
            programListNode.AppendChild(programNode);

            Console.WriteLine(FormatXML(stopPgxFile));
            Console.WriteLine("--------------------END--------------------");
            Console.ReadKey();
        }
        #endregion
    }

    /// <summary>
    /// 
    /// </summary>
    class XMLReadWrite
    {
        static void Main(string[] args)
        {
            PositionDataXmlfileWritter xmlwr = new PositionDataXmlfileWritter();

            //xmlwr.WriteDataFile();
            //xmlwr.WirteStartPgxFile();
            //xmlwr.WirteStopPgxFile();
            //xmlwr.WriteProgramFile();

            Test.testParent();
            Console.ReadKey();
            Test t = new Test();
            t.testParetAtt();
            Console.ReadKey();
        }
    }
}



//XMLRobotInfoFile xmlRobInfo = new XMLRobotInfoFile("C:\\RobotInfo.xml");
// xmlRobInfo.WriteRobotInfoBlock();
//string twoBooksPath = "C:\\2books.xml";
//xmlRobInfo.ReadTwobooks(twoBooksPath);
//string suitePath = "C:\\suite.xml";
//xmlRobInfo.ReadSuite(suitePath);
//xmlRobInfo.TestCreateNode();