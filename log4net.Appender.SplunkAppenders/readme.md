log4net.Appender.SplunkAppenders

	<configSections>
		...
		<section name="log4net" type="log4net.Config.Log4NetConfigurationSectionHandler, log4net" />
		...
	</configSections>
	...
	<log4net debug="false">
		<appender name="TraceAppender" type="log4net.Appender.TraceAppender">
		  <layout type="log4net.Layout.PatternLayout">
			<conversionPattern value="%message %exception %aspnet-request" />
		  </layout>
		  <filter type="log4net.Filter.LevelRangeFilter">
			<levelMin value="ALL" />
			<levelMax value="EMERGENCY" />
		  </filter>
		</appender>
		<appender name="SplunkAppender" type="log4net.Appender.SplunkAppenders.SplunkAppender, log4net.Appender.SplunkAppenders">
		  <Application value="Demo" />
		  <SplunkHost value="localhost" />
		  <UserName value="log4net" />
		  <Password value="log4netpass" />
		  <IndexName value="log4net" />
		  <Async value="false" />
		  <EnvironmentVariablesLevel value="INFO" />
		  <layout type="log4net.Layout.PatternLayout">
			<conversionPattern value="%message %exception %aspnet-request" />
		  </layout>
		  <parameter>
			<parameterName value="StackTrace" />
			<layout type="log4net.Layout.PatternLayout">
			  <conversionPattern value="%stacktrace" />
			</layout>
		  </parameter>
		  <parameter>
			<parameterName value="Exception" />
			<layout type="log4net.Layout.PatternLayout">
			  <conversionPattern value="%exception" />
			</layout>
		  </parameter>
		  <filter type="log4net.Filter.LevelRangeFilter">
			<levelMin value="ALL" />
			<levelMax value="EMERGENCY" />
		  </filter>
		</appender>
		<root>
		  <level value="ALL" />
		  <appender-ref ref="TraceAppender" />
		  <appender-ref ref="SplunkAppender" />
		</root>
		<logger name="Splunk" additivity="false">
		  <level value="ALL" />
		  <appender-ref ref="SplunkAppender" />
		</logger>
	</log4net>
	...


Happy logging...