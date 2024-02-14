●新規ダイアログの作成

１．作成したいダイアログの、Model用のクラスを作成する。「***Dialogue」などがよい。
２．作成したModel用クラスに、クラス「Dialogue」を継承、インタフェース「IDialogue」を実装する。
３．UIとの連携を担う、Presenter用のクラスを作成する。「***DialoguePresenter」などがよい。
４．作成したPresenter用クラスに、クラス「DialoguePresenter」を継承する。