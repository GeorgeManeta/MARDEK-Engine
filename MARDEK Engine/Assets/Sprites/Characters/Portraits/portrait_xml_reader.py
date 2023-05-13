"""
Usage:
Export a portrait DefineSprite_[number] to SVG. Also export it to SWF, then open the SWF in JPEXS and export the SWF to XML.

Has various functions for operating on the XML file/SVG folder.
"""

# For renaming SVGs
import os

# For parsing XML file
# Formerly used minidom and ElementTree but those suck, lxml is better
from lxml import etree as ET

# Reads the frames and framelabels in the xml and returns a list where the ith element is the name/label of the (i+1)th frame in the DefineSprite
def readFrames(fileName = ""):

    if fileName == "":
        fileName = input("Enter name of xml file: ")
        print("File:", file)

    DOMTree = ET.parse(fileName)
    frameCount = 0
    frameList = []

    # Find the actual DefineSprite in the greater XML representing the entire SWF
    # There is no method for finding node by attribute, so iterate over them
    items = DOMTree.findall(".//{*}item")
    for item in items:
        if item.get("type") == "DefineSpriteTag":
            print("DefineSprite item found: spriteId = ", item.get("spriteId"))

            # Get all the items in the DefineSprite that are frames, and their labels
            # The FrameLabelTag always shows up before the corresponding ShowFrameTag
            subItems = item.findall(".//{*}item")
            currentFrameLabel = ""
            for subItem in subItems:
                if subItem.get("type") == "FrameLabelTag":
                    currentFrameLabel = subItem.get("name")

                elif subItem.get("type") == "ShowFrameTag":
                    if currentFrameLabel == "":
                        # e.g. if the first frame has no name, its name will be "1"
                        frameList.append(str(frameCount + 1))
                    else:
                        frameList.append(currentFrameLabel)

                    frameCount += 1
                    currentFrameLabel = ""

            break

    return frameList

"""
folderName is the name of the folder containing SVGs exported from the DefineSprite corresponding to frameList.
These SVGs will be renamed from [frame number].svg to [frame_name]_[frame_number].svg.
E.g. 16.svg will become bald_16.svg if frame 16 has label "bald".
"""
def renameSVGs(frameList, folderName = ""):
    if folderName == "":
        folderName = input("Enter name of folder containing exported SVGs: ")
        print("Folder name:", folderName)

    for i in range(len(frameList)):
        frameNumber = str(i + 1)

        if frameList[i] != frameNumber:
            svgName = frameNumber + ".svg"
            fullSvgName = os.path.join(folderName, svgName)

            if os.path.isfile(fullSvgName):
                newSvgName = frameList[i] + "_" + frameNumber + ".svg"
                os.rename(fullSvgName, os.path.join(folderName, newSvgName))
            else:
                print("WARNING!", svgName, "is not a file!")

    print("Files in", folderName, "renamed.")

"""
Unity's SVG importer does not support SVGs exported by JPEXS through a DefineSprite and not a DefineShape
Because it uses `use data-characterId` and/or `xlink:href="#shapeX"` (?)
Therefore, we edit all the SVGs and replace each `use` element with the shape definition it's referencing
"""
def reformatSVGs(folderName = ""):
    if folderName == "":
        folderName = input("Enter name of folder containing exported SVGs: ")
        print("Folder name:", folderName)

    svgs = [file for file in sorted(os.listdir(folderName)) if file.endswith(".svg")]
    for svg in svgs:
        print("File:", svg)

        svgName = os.path.join(folderName, svg) 
        DOMTree = ET.parse(svgName)
        SVGRoot = DOMTree.getroot()
        shapes = []

        # Find definition of each shape in <defs> section
        defs_list = SVGRoot.findall(".//{*}defs")
        if (len(defs_list) > 1):
            print("WARNING - more than 1 <defs>!")
        elif (len(defs_list) == 0):
            print("WARNING - no <defs>!")
        else:
            defs = defs_list[0]

            for g in defs.findall(".//{*}g"):
                print("g id:", g.get("id"))
                if g.get("id").startswith("shape"):
                    shapes.append(g)

            """
            We want to get rid of <use> but keep its attributes,
            so we turn the <use> element into a <g> element and make each shape
            a child of the new <g>
            """
            use_list = SVGRoot.findall(".//{*}g")[0].findall(".//{*}use")
            if (len(use_list) == 0):
                print("WARNING: No <use> element!")
            else:
                for i in range(len(shapes)):
                    use_element = use_list[i]
                    use_element.tag = "g"
                    use_element.append(shapes[i])
                    #print("Shape added:", shapes[i].get("id"))

            DOMTree.write(svgName)

    print("SVGs in", folderName, "formatted.")

def run_all():
    files = [f for f in sorted(os.listdir()) if os.path.isdir(f)]
    for folderName in files:
        print("folder:", folderName)

        fileName = os.path.join(folderName, "swf.xml")
        try:
            frameList = readFrames(fileName)
            print("List of frames:")
            print(frameList)
            renameSVGs(frameList, folderName)
        except:
            print("SWF not detected")

        reformatSVGs(folderName)

if __name__ == "__main__":
    run_all()

if 1 == 2:
    # Replace with name of xml you want to read
    fileName = r"C:\Users\account\Documents\MardekDev\MARDEK-Engine\MARDEK Engine\Assets\Sprites\Characters\Portraits\DefineSprite_883_neck_male\swf.xml"
    frameList = readFrames(fileName)
    print("List of frames:")
    print(frameList)

    folderName = r"C:\Users\account\Documents\MardekDev\MARDEK-Engine\MARDEK Engine\Assets\Sprites\Characters\Portraits\DefineSprite_883_neck_male"
    renameSVGs(frameList, folderName)

    reformatSVGs(folderName)
