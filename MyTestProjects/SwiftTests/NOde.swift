import Foundation
import UIKit

struct Node{
    let uid: String
    let title: String
    let content: String
    let color: UIColor
    enum importance {
        case unimportant
        case usual
        case important
    }
    let SelfDestructionDate: Date?
    
    init(uid: String  = UUID().uuidString, title: String, content: String, color: UIColor = [UIColor whiteColor], importance: importance, SelfDestructionDate: Date) {
        self.uid = uid
        self.title = title
        self.content = content
        self.color = color
        self.SelfDestructionDate = SelfDestructionDate
    }
    
    init(uid: String  = UUID().uuidString, title: String, content: String, color: UIColor = [UIColor whiteColor], importance: String) {
        self.uid = uid
        self.title = title
        self.content = content
        self.color = color
        self.SelfDestructionDate = nil
    }
}

