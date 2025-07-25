import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flutter_unity_widget/flutter_unity_widget.dart';

void main() {
  WidgetsFlutterBinding.ensureInitialized();

  // Set full screen mode (immersive)
  SystemChrome.setEnabledSystemUIMode(SystemUiMode.immersiveSticky);

  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  @override
  Widget build(BuildContext context) {
    return const MaterialApp(
      debugShowCheckedModeBanner: false,
      home: UnityFullScreen(),
    );
  }
}

class UnityFullScreen extends StatefulWidget {
  const UnityFullScreen({super.key});

  @override
  State<UnityFullScreen> createState() => _UnityFullScreenState();
}

class _UnityFullScreenState extends State<UnityFullScreen> {
  UnityWidgetController? _unityWidgetController;

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: UnityWidget(
        onUnityCreated: onUnityCreated,
        onUnityMessage: onUnityMessage,
        onUnitySceneLoaded: onUnitySceneLoaded,
        fullscreen: true,
      ),
    );
  }

  void onUnityCreated(UnityWidgetController controller) {
    _unityWidgetController = controller;
  }

  void onUnityMessage(dynamic message) {
    debugPrint('Received message from Unity: $message');
  }

  void onUnitySceneLoaded(SceneLoaded? sceneInfo) {
    if (sceneInfo != null) {
      debugPrint(
        'Scene Loaded: ${sceneInfo.name}, Index: ${sceneInfo.buildIndex}',
      );
    }
  }
}
