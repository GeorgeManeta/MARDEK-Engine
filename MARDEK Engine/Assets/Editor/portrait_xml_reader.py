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

# For changing SVG import settings in .svg.meta files
import re

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

"""
Set SVG import settings for all .svg.meta files in folderName.

The SVG is expected to have "Textured Sprite" as its Generated Asset Type.

TextureSize is max(width, height) * scaleFactor --- scaleFactor = 3 by default
but can be changed. Legion's portrait sprites are scaled by 7, for example.
"""
def svg_import_settings(folderName = "", scaleFactor = 3):
    if folderName == "":
        folderName = input("Enter name of folder containing exported SVGs: ")
        print("Folder name:", folderName)

    for metadata in [f for f in sorted(os.listdir(folderName)) if f.endswith(".svg.meta")]:

        metadata = os.path.join(folderName, metadata)

        # get texture size of imported SVG
        svgName = metadata[:-5]
        DOMTree = ET.parse(svgName)
        SVGRoot = DOMTree.getroot()
        height = float(SVGRoot.get("height")[:-2])
        width = float(SVGRoot.get("width")[:-2])
        textureSize = round(max(height, width)) * scaleFactor
        print("read:", svgName, "height:", height, "width:", width, "textureSize:", textureSize)

        file_text = ""
        with open(metadata, "r") as f:
            file_text = f.read()

            # set Pixels per Unit to 1
            file_text = re.sub(r'svgPixelsPerUnit: [0-9]+\n', r'svgPixelsPerUnit: 1\n', file_text)

            # set Texture Size to correct value
            file_text = re.sub(r'textureSize: [0-9]+\n', r'textureSize: ' + str(textureSize) + r'\n', file_text)

        with open(metadata, "w") as f:
            f.write(file_text)

"""
Increases the stroke-width of all .svg files in folderName.

Needed because Flash displays SVGs with larger stroke-width than they actually
have, so we increase it to make the Unity SVGs look like the Flash versions.

All stroke-widths are multiplied by scaleFactor, then set to 1 px if they're less.
scaleFactor was determined by trial and error.
"""
def increase_stroke_width(folderName = ""):
    scaleFactor = 2

    if folderName == "":
        folderName = input("Enter name of folder containing SVGs: ")
        print("Folder name:", folderName)

    for svg in [f for f in sorted(os.listdir(folderName)) if f.endswith(".svg")]:
        print("SVG", svg)

        svg = os.path.join(folderName, svg)
        DOMTree = ET.parse(svg)
        SVGRoot = DOMTree.getroot()
        shapes = []

        # Find definition of each shape in <defs> section
        for node in SVGRoot.iter():

            #print("node tag:", node.tag)
            width = node.get("stroke-width")
            if width is not None:
                width = float(width)

                new_width = width * scaleFactor
                if (new_width < 1.5):
                    new_width = 1.5

                new_width = str(new_width)

                node.set("stroke-width", new_width)
                print("stroke-width changed:", width, new_width)

            DOMTree.write(svg)

"""
Renames and reformats the files in every folder of this program's current working directory.

Does not change the SVG import settings.
You will need to run svg_import_settings() seperately.
"""
def run_all():
    files = [f for f in sorted(os.listdir()) if os.path.isdir(f)]
    for folderName in files:
        print("folder:", folderName)

        fileName = os.path.join(folderName, "swf.xml")
        try:
            frameList = readFrames(fileName)
            renameSVGs(frameList, folderName)
        except:
            print("SWF not detected")

        reformatSVGs(folderName)

def increase_stroke_width_all():
    files = [f for f in sorted(os.listdir()) if os.path.isdir(f)]
    for folderName in files:
        increase_stroke_width(folderName)

if __name__ == "__main__":
    increase_stroke_width_all()
