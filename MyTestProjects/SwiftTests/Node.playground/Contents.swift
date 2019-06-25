import Cocoa
import UIKit

var color =

var black: UIColor {get}

struct Node{
    let uid: String
    let title: String?
    let content: String?
    let color: String
    enum importance {
        case unimportant
        case usual
        case important
    }
    let SelfDestructionDate: Date?
    
    init(uid: String  = UUID().uuidString, title: String, content: String, color: String = "White", importance: importance, SelfDestructionDate: Date) {
        self.uid = uid
        self.title = title
        self.content = content
        self.color = color
        self.SelfDestructionDate = SelfDestructionDate
    }
    
    init(uid: String  = UUID().uuidString, title: String, content: String, color: String = "White", importance: String) {
        self.uid = uid
        self.title = title
        self.content = content
        self.color = color
        self.SelfDestructionDate = nil
    }
}
